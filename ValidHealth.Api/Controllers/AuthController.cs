using AutoMapper;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using ValidHealth.Api.Dtos;
// using ValidHealth.Api.Helpers;
using ValidHealth.Data;
using ValidHealth.Domain.Entities;
using ValidHealth.Api.Middleware;
using System.IO;
// using NLog;
using Newtonsoft.Json;

namespace ValidHealth.Api.Controllers
{
  [Route("api/auth")]
  public class AuthController : Controller
  {
    private readonly ValidHealthContext _context;
    private readonly UserManager<User> _userMgr;
    private readonly RoleManager<Role> _roleMgr;
    private readonly IPasswordHasher<User> _hasher;
    private readonly SignInManager<User> _signInMgr;
    private readonly IConfigurationRoot _config;
    private readonly IMapper _mapper;
    private readonly int _secMgr;
    protected readonly ILoggerManager _logger;

    public AuthController(ValidHealthContext context,
      UserManager<User> userMgr,
      RoleManager<Role> roleMgr,
      IPasswordHasher<User> hasher,
      SignInManager<User> signInMgr,
      IConfigurationRoot config,
      IMapper mapper, ILoggerManager logger = null)
    {
      _context = context;
      _userMgr = userMgr;
      _roleMgr = roleMgr;
      _hasher = hasher;
      _signInMgr = signInMgr;
      _config = config;
      _mapper = mapper;
      // _logger = NLog.LogManager.GetCurrentClassLogger();
      if (null != logger)
      {
        _logger = logger;
      }
      _secMgr = new FileInfo(@"appsettings.json").CreationTime.Millisecond;
    }

// [DllImport("user32.dll")]
// public static extern int MessageBox(IntPtr hWnd, String text, String caption, int options);

    [HttpPost("token")]
    public async Task<IActionResult> CreateToken([FromBody]/* [FromForm] */ CredentialResource dto)
    {
      _logger.LogInformation(_config["ConnectionStrings:KlinikConnection"]);
      if (dto == null) return BadRequest();

      if (!ModelState.IsValid) return new UnprocessableEntityResult();

      //var user = await _userMgr.FindByNameAsync(dto.UserName);
      var user = await _userMgr.Users
        // .Include(u => u.Roles)
        .Include(u => u.Klinik)
        .SingleOrDefaultAsync(u => u.UserName == dto.UserName);

      if (user == null)
      {
        return StatusCode((int)HttpStatusCode.BadRequest, "UserName tidak ditemukan");
        // return BadRequest();
      }

      if (_hasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password) == PasswordVerificationResult.Failed)
      {
        return StatusCode((int)HttpStatusCode.Unauthorized, "Password Invalid");
        // return Unauthorized();
      }

      try
      {
        // var userRoles = ((IdentityUser<long>) user).Roles;

        var claims = new List<Claim>
        {
          new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
          new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
          new Claim("name", string.IsNullOrWhiteSpace(user.FullName) ? "" :
              (string.IsNullOrWhiteSpace(user.Email) ? "" : user.Email)),
          new Claim(JwtRegisteredClaimNames.Email, string.IsNullOrWhiteSpace(user.Email) ? "" : user.Email),
          new Claim("klinikId", user.KlinikId.GetValueOrDefault().ToString()),
          new Claim("klinikName", user.Klinik?.Nama ?? ""),
          new Claim("Konfig", (user.Klinik?.Konfig ?? 1).ToString()),
          new Claim(JwtRegisteredClaimNames.NameId, _secMgr.ToString()),
          new Claim("klinikLogo", user.Klinik?.LogoFilename ?? ""),
          new Claim("OrganLayan", (user.Klinik?.OrganLayan).ToString()),
          new Claim("userId", user.Id.ToString())
        };

        using (DbConnection conn = _context.Database.GetDbConnection())
        {
          var sql =
            "SELECT " +
            "  RoleId " +
            "FROM " +
            "  UserRole " +
            "WHERE UserId = @UserId ";

          var qr = await conn.QueryAsync(sql, new { UserId = user.Id });

          claims.AddRange(qr.AsList().Select(userRole =>
          {
            return new Claim("roleId", userRole.RoleId.ToString());
          }));
        }

        var token = new JwtSecurityToken(
          issuer: _config["Tokens:Issuer"],
          audience: _config["Tokens:Audience"],
          claims: claims,
          expires: DateTime.UtcNow.AddMinutes(60),
          signingCredentials: SigningCredentials()
        );

        return Ok(new
        {
          token = new JwtSecurityTokenHandler().WriteToken(token),
          expiration = token.ValidTo
        });
      }
      catch (Exception e)
      {
        if (e.InnerException != null)
        {
          _logger.LogError(e.InnerException.Message, e);
          return StatusCode((int)HttpStatusCode.InternalServerError, e.InnerException.Message);
        }
        else
        {
          _logger.LogError(e.Message, e);
          return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
        }
      }
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
      string s = "Something error";
      var v = new { now = DateTime.Now };
      await Task.Run(() =>
      {
        s = JsonConvert.SerializeObject(v);
      });
      return Ok(s);
    }

    private SigningCredentials SigningCredentials()
    {
      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));

      var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
      return creds;
    }
  }
}
