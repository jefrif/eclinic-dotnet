using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ValidHealth.Data.Models;
using ValidHealth.Domain.Entities;
using ValidHealth.Domain.Enums;

namespace ValidHealth.Data.Repository
{
  public interface IJasaLayananRepository : IRepository<JasaLayanan>
  {
    Task<IEnumerable<TreeTableModel<JasaLayananTreeModel>>> GetAllJasaLayanan(long klinikId, long kelasLayananId, CommonFilters filters);
    Task<TreeTableModel<JasaLayananTreeModel>> GetJasaLayanan(long klinikId, long kelasLayananId, long id, CommonFilters filters);
    Task<JasaLayanan> GetJasaLayananByFixedId(long klinikId, long kelasLayananId, JnsJasa fixedId);

  }

  public class JasaLayananRepository : Repository<JasaLayanan>, IJasaLayananRepository
  {
    public JasaLayananRepository(ValidHealthContext context) : base(context)
    {
    }

    public ValidHealthContext AppContext => Context as ValidHealthContext;

    public async Task<IEnumerable<TreeTableModel<JasaLayananTreeModel>>> GetAllJasaLayanan(long klinikId, long kelasLayananId, CommonFilters filters)
    {
      var models = await AppContext.JasaLayanan
        .Where(j => j.ParentId == null && j.KlinikId == klinikId)
        .OrderBy(j => j.Id)
        .Select(j => new TreeTableModel<JasaLayananTreeModel>
        {
          Data = new JasaLayananTreeModel
          {
            Id = j.Id,
            UnitBarangJasaId = j.TarifJasa.Where(t => t.KelasLayananId == kelasLayananId).Select(t => t.UnitBarangJasaId).SingleOrDefault(),
            TarifJasaId = j.TarifJasa.Where(t => t.KelasLayananId == kelasLayananId).Select(t => t.Id).SingleOrDefault(),
            ParentId = j.ParentId,
            Kode = j.Kode,
            Nama = j.Nama,
            FixedId = j.FixedId,
            AkunBlud = j.AkunBlud,
            Gender = j.Gender,
            Satuan = j.TarifJasa.Where(t => t.KelasLayananId == kelasLayananId).Select(t => t.UnitBarangJasa.Nama).SingleOrDefault(),
            JasaRs = j.TarifJasa.Where(t => t.KelasLayananId == kelasLayananId).Select(t => t.JasaRs).SingleOrDefault(),
            JasaMedik = j.TarifJasa.Where(t => t.KelasLayananId == kelasLayananId).Select(t => t.JasaMedik).SingleOrDefault(),
            BahanHabisPakai = j.TarifJasa.Where(t => t.KelasLayananId == kelasLayananId).Select(t => t.BahanHabisPakai).SingleOrDefault(),
            HargaSatuan = j.TarifJasa.Where(t => t.KelasLayananId == kelasLayananId)
                .Select(t => t.HargaSatuan).SingleOrDefault(),
            KodeTindakBpjs = j.TarifJasa.Where(t => t.KelasLayananId == kelasLayananId)
                .Select(t => t.KodeTindakBpjs).SingleOrDefault()
          }
        }).ToListAsync();

      models.ForEach(m => PopulateChildren(m, kelasLayananId, filters));

      return models;
    }

    public async Task<TreeTableModel<JasaLayananTreeModel>> GetJasaLayanan(long klinikId, long kelasLayananId, long id, CommonFilters filters)
    {
      var model = await AppContext.JasaLayanan
        .Where(j => j.KlinikId == klinikId && j.Id == id)
        .OrderBy(j => j.Parent.Id)
        .Select(j => new TreeTableModel<JasaLayananTreeModel>
        {
          Data = new JasaLayananTreeModel
          {
            Id = j.Id,
            ParentId = j.ParentId,
            Kode = j.Kode,
            Nama = j.Nama,
            FixedId = j.FixedId,
            AkunBlud = j.AkunBlud,
            Gender = j.Gender,
            UnitBarangJasaId = j.TarifJasa.Where(t => t.KelasLayananId == kelasLayananId).Select(t => t.UnitBarangJasaId).SingleOrDefault(),
            TarifJasaId = j.TarifJasa.Where(t => t.KelasLayananId == kelasLayananId).Select(t => t.Id).SingleOrDefault(),
            Satuan = j.TarifJasa.Where(t => t.KelasLayananId == kelasLayananId).Select(t => t.UnitBarangJasa.Nama).SingleOrDefault(),
            JasaRs = j.TarifJasa.Where(t => t.KelasLayananId == kelasLayananId).Select(t => t.JasaRs).SingleOrDefault(),
            JasaMedik = j.TarifJasa.Where(t => t.KelasLayananId == kelasLayananId).Select(t => t.JasaMedik).SingleOrDefault(),
            BahanHabisPakai = j.TarifJasa.Where(t => t.KelasLayananId == kelasLayananId).Select(t => t.BahanHabisPakai).SingleOrDefault(),
            HargaSatuan = j.TarifJasa.Where(t => t.KelasLayananId == kelasLayananId)
                .Select(t => t.HargaSatuan).SingleOrDefault(),
            KodeTindakBpjs = j.TarifJasa.Where(t => t.KelasLayananId == kelasLayananId)
                .Select(t => t.KodeTindakBpjs).SingleOrDefault()
          }
        })
        .SingleOrDefaultAsync();

      PopulateChildren(model, kelasLayananId, filters);

      return model;
    }

    public async Task<JasaLayanan> GetJasaLayananByFixedId(long klinikId, long kelasLayananId, JnsJasa fixedId)
    {
      var model = await AppContext.JasaLayanan
        .Where(j => j.KlinikId == klinikId &&
                    j.FixedId == fixedId &&
                    j.TarifJasa.Any(t => t.KelasLayananId == kelasLayananId))
        .Include(j => j.TarifJasa)
        .FirstOrDefaultAsync();

      return model;
    }

    private void PopulateChildren(TreeTableModel<JasaLayananTreeModel> parent, long kelasLayananId, CommonFilters filters)
    {
      var query = AppContext.JasaLayanan
        .Where(j => j.ParentId == parent.Data.Id)
        .OrderBy(j => j.Id)
        .Select(j => new TreeTableModel<JasaLayananTreeModel>
        {
          Data = new JasaLayananTreeModel
          {
            Id = j.Id,
            ParentId = j.ParentId,
            UnitBarangJasaId = j.TarifJasa.Where(t => t.KelasLayananId == kelasLayananId)
              .Select(t => t.UnitBarangJasaId).SingleOrDefault(),
            TarifJasaId =
              j.TarifJasa.Where(t => t.KelasLayananId == kelasLayananId).Select(t => t.Id).SingleOrDefault(),
            Kode = j.Kode,
            Nama = j.Nama,
            FixedId = j.FixedId,
            AkunBlud = j.AkunBlud,
            Gender = j.Gender,
            Satuan = j.TarifJasa.Where(t => t.KelasLayananId == kelasLayananId).Select(t => t.UnitBarangJasa.Nama)
              .SingleOrDefault(),
            JasaRs = j.TarifJasa.Where(t => t.KelasLayananId == kelasLayananId).Select(t => t.JasaRs).SingleOrDefault(),
            JasaMedik = j.TarifJasa.Where(t => t.KelasLayananId == kelasLayananId).Select(t => t.JasaMedik)
              .SingleOrDefault(),
            BahanHabisPakai = j.TarifJasa.Where(t => t.KelasLayananId == kelasLayananId).Select(t => t.BahanHabisPakai)
              .SingleOrDefault(),
            HargaSatuan = j.TarifJasa.Where(t => t.KelasLayananId == kelasLayananId).Select(t => t.HargaSatuan)
              .SingleOrDefault(),
            KodeTindakBpjs = j.TarifJasa.Where(t => t.KelasLayananId == kelasLayananId)
                .Select(t => t.KodeTindakBpjs).SingleOrDefault()
          }
        })
        .AsQueryable();

      var filtersColumnMap = new Dictionary<string, Func<TreeTableModel<JasaLayananTreeModel>, bool>>
      {
        ["noZeroLeafs"] = j => j.Data.JasaRs != 0 || j.Data.JasaMedik != 0 || j.Data.BahanHabisPakai != 0
      };

      if (!string.IsNullOrWhiteSpace(filters.FilterBy))
      {
        query = query.AsEnumerable()
          .Where(filtersColumnMap[filters.FilterBy])
          .AsQueryable();
      }

      var children = query.ToList();

      parent.Children = children;

      parent.Children.ForEach(child => PopulateChildren(child, kelasLayananId, filters));
    }
  }
}
