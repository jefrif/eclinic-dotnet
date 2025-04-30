using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ValidHealth.Data.Repository;

namespace ValidHealth.Api.Controllers
{
  [Route("api/accessviewtree/role/{roleId}/accessview")]
  [Authorize]
  public class AccessViewRoleTreeController : Controller
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AccessViewRoleTreeController(IUnitOfWork unitOfWork,
      IMapper mapper)
    {
      _unitOfWork = unitOfWork;
      _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Get(long roleId)
    {
      var models = await _unitOfWork.AccessView.GetAccessViewTree(roleId);

      return Ok(models);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(long roleId, long id)
    {
      var model = await _unitOfWork.AccessView.GetAccessViewTree(roleId, id);

      if (model == null) return NotFound();

      return Ok(model);
    }
  }
}
