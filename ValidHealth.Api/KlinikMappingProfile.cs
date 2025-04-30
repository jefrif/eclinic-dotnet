using AutoMapper;
using System;
using System.Linq;
using ValidHealth.Api.Dtos;
using ValidHealth.Api.Resolver;
using ValidHealth.Data.Models;
using ValidHealth.Domain.Entities;
using ValidHealth.Domain.Enums;

namespace ValidHealth.Api
{
  public class KlinikMappingProfile : Profile
  {
    public KlinikMappingProfile()
    {
      CreateMap<Anggota, AnggotaDto>()
        .ForMember(d => d.KecamatanId, opt => opt.MapFrom(s => s.AlamatPelengkap.KecamatanId))
        .ForMember(d => d.KelurahanId, opt => opt.MapFrom(s => s.AlamatPelengkap.KelurahanId))
        .ForMember(d => d.Rt, opt => opt.MapFrom(s => s.AlamatPelengkap.Rt))
        .ForMember(d => d.Rw, opt => opt.MapFrom(s => s.AlamatPelengkap.Rw))
        .ForMember(d => d.KabKotaId, opt => opt.MapFrom(s => s.AlamatPelengkap.KabKotaId))
        .ForMember(d => d.ProvinsiId, opt => opt.MapFrom(s => s.AlamatPelengkap.ProvinsiId))
        .ForMember(d => d.Url, opt => opt.MapFrom<AnggotaUrlResolver>());

      CreateMap<AnggotaInputDto, Anggota>()
        .ForMember(d => d.AlamatPelengkap, opt => opt.Ignore());

      CreateMap<AnggotaUpdateDto, Anggota>()
        .ForMember(d => d.AlamatPelengkap, opt => opt.MapFrom(s => new AlamatPelengkap
        {
          KecamatanId = s.KecamatanId,
          KelurahanId = s.KelurahanId,
          Rt = s.Rt,
          Rw = s.Rw,
          KabKotaId = s.KabKotaId,
          ProvinsiId = s.ProvinsiId
        }));

      CreateMap<Klinik, KlinikDto>()
        .ForMember(d => d.Url, opt => opt.MapFrom<KlinikUrlResolver>())
        .ForMember(d => d.KecamatanId, opt => opt.MapFrom(s => s.AlamatPelengkap.KecamatanId))
        .ForMember(d => d.KelurahanId, opt => opt.MapFrom(s => s.AlamatPelengkap.KelurahanId))
        .ForMember(d => d.Rt, opt => opt.MapFrom(s => s.AlamatPelengkap.Rt))
        .ForMember(d => d.Rw, opt => opt.MapFrom(s => s.AlamatPelengkap.Rw))
        .ForMember(d => d.KabKotaId, opt => opt.MapFrom(s => s.AlamatPelengkap.KabKotaId))
        .ForMember(d => d.ProvinsiId, opt => opt.MapFrom(s => s.AlamatPelengkap.ProvinsiId));

      CreateMap<KlinikInputDto, Klinik>()
        .ForMember(d => d.AlamatPelengkap, opt => opt.MapFrom(s => new AlamatPelengkap
        {
          KecamatanId = s.KecamatanId,
          KelurahanId = s.KelurahanId,
          Rt = s.Rt,
          Rw = s.Rw,
          KabKotaId = s.KabKotaId,
          ProvinsiId = s.ProvinsiId
        }));

      CreateMap<Pasien, PasienDto>()
        .ForMember(d => d.Url, opt => opt.MapFrom<PasienUrlResolver>());

      CreateMap<PasienInputDto, Pasien>();

      CreateMap<Provinsi, KeyValuePairDto>();

      CreateMap<KabKota, KabKotaDto>();

      CreateMap<Kecamatan, KecamatanDto>();

      CreateMap<Kelurahan, KelurahanDto>();

      CreateMap<Asuransi, AsuransiDto>()
        .ForMember(d => d.Url, opt => opt.MapFrom<AsuransiUrlResolver>());
      CreateMap<AsuransiCreateDto, Asuransi>();

      CreateMap<PesertaAsuransi, PesertaAsuransiDto>();
      CreateMap<PesertaAsuransiCreateDto, PesertaAsuransi>();
      CreateMap<PesertaAsuransiUpdateDto, PesertaAsuransi>();

      CreateMap<InstansiPerujuk, InstansiPerujukDto>()
        .ForMember(d => d.Url, opt => opt.MapFrom<InstansiPerujukUrlResolver>());
      CreateMap<InstansiPerujukCreateDto, InstansiPerujuk>();

      CreateMap<Dokter, DokterDto>()
        .ForMember(d => d.Url, opt => opt.MapFrom<DokterUrlResolver>());
      CreateMap<DokterCreateDto, Dokter>();
      CreateMap<DokterUpdateDto, Dokter>();

      CreateMap<Poliklinik, PoliklinikDto>()
        .ForMember(d => d.Url, opt => opt.MapFrom<PoliklinikUrlResolver>());
      CreateMap<PoliklinikCreateDto, Poliklinik>();

      CreateMap<UnitKlinik, UnitKlinikDto>()
        .ForMember(d => d.Url, opt => opt.MapFrom<UnitKlinikUrlResolver>())
        .ForMember(d => d.NamaPoliklinik, opt => opt.MapFrom(s => s.Poliklinik.Nama));
      CreateMap<UnitKlinikCreateDto, UnitKlinik>();

      CreateMap<DokterUnitKlinik, DokterUnitKlinikDto>();
      CreateMap<DokterUnitKlinikCreateDto, DokterUnitKlinik>();

      CreateMap<PelayananRj, PelayananRjDto>()
        .ForMember(d => d.Url, opt => opt.MapFrom<PelayananRjUrlResolver>())
        .ForMember(d => d.DokterUnit, opt => opt.MapFrom(s => s.DokterUnitKlinik));
      CreateMap<PelayananRjCreateDto, PelayananRj>();
      CreateMap<PelayananRj, PelayananRjCreateDto>();

      CreateMap<Rujukan, RujukanDto>()
        .ForMember(d => d.Url, opt => opt.MapFrom<RujukanUrlResolver>());
      CreateMap<RujukanCreateDto, Rujukan>();

      CreateMap<Role, RoleDto>();
      CreateMap<RoleCreateDto, Role>();

      CreateMap<JasaLayanan, JasaLayananParentDto>()
        .ForMember(j => j.Url, opt => opt.MapFrom<JasaLayananUrlResolver>());
      CreateMap<JasaLayanan, JasaLayananChildrenDto>()
        .ForMember(j => j.Url, opt => opt.MapFrom<JasaLayananUrlResolver>())
        .ForMember(d => d.Label, opt => opt.MapFrom(s => s.Nama))
        .ForMember(d => d.Data, opt => opt.MapFrom(s => s.Id));
      CreateMap<JasaLayanan, JasaLayananDto>()
        .ForMember(j => j.Url, opt => opt.MapFrom<JasaLayananUrlResolver>());
      CreateMap<JasaLayananCreateDto, JasaLayanan>();
      CreateMap<JasaLayananUpdateDto, JasaLayanan>();

      CreateMap<KelasLayanan, KelasLayananDto>()
        .ForMember(k => k.Url, opt => opt.MapFrom<KelasLayananUrlResolver>());
      CreateMap<KelasLayananDto, KelasLayanan>();

      CreateMap<UnitBarangJasa, UnitBarangJasaDto>()
        .ForMember(u => u.Url, opt => opt.MapFrom<UnitBarangJasaUrlResolver>());
      CreateMap<UnitBarangJasaCreateDto, UnitBarangJasa>();

      CreateMap<TarifJasa, TarifJasaDto>()
        .ForMember(d => d.Url, opt => opt.MapFrom<TarifJasaUrlResolver>());
      CreateMap<TarifJasa, TarifJasaTagihanDto>();
      CreateMap<TarifJasaCreateDto, TarifJasa>();

      CreateMap<JasaLayananRj, JasaLayananRjDto>()
        .ForMember(d => d.Url, opt => opt.MapFrom<JasaLayananRjUrlResolver>())
        .ForMember(d => d.TarifJasaKelasLayananId, opt => opt.MapFrom(s => s.TarifJasa.KelasLayananId))
        .ForMember(d => d.JasaLayananNama, opt => opt.MapFrom(s => s.TarifJasa.JasaLayanan.Nama))
        .ForMember(d => d.FixedId, opt => opt.MapFrom(s => s.TarifJasa.JasaLayanan.FixedId))
        .ForMember(d => d.UnitBarangJasaNama, opt => opt.MapFrom(s => s.TarifJasa.UnitBarangJasa.Nama))
        .ForMember(d => d.TarifJasaHargaSatuan, opt => opt.MapFrom(s => s.TarifJasa.HargaSatuan))
        .ForMember(d => d.Jumlah, opt => opt.MapFrom(c => c.Kuantitas * c.TarifJasa.HargaSatuan))
        .ForMember(d => d.KodeTindakBpjs, opt => opt.MapFrom(c => c.TarifJasa.KodeTindakBpjs));
      CreateMap<JasaLayananRjCreateDto, JasaLayananRj>();

      CreateMap<PelayananRjFiltersDto, PelayananRjFilters>();

      CreateMap<KeluhanUtama, KeluhanUtamaDto>()
        .ForMember(d => d.Url, opt => opt.MapFrom<KeluhanUtamaUrlResolver>());
      CreateMap<KeluhanUtamaCreateDto, KeluhanUtama>();
      CreateMap<KeluhanUtamaUpdateDto, KeluhanUtama>();

      CreateMap<RiwayatPenyakit, RiwayatPenyakitDto>()
        .ForMember(d => d.Url, opt => opt.MapFrom<RiwayatPenyakitUrlResolver>());
      CreateMap<RiwayatPenyakitCreateDto, RiwayatPenyakit>();
      CreateMap<RiwayatPenyakitUpdateDto, RiwayatPenyakit>();

      CreateMap<RiwayatLain, RiwayatLainDto>()
        .ForMember(d => d.Url, opt => opt.MapFrom<RiwayatLainUrlResolver>());
      CreateMap<RiwayatLainCreateDto, RiwayatLain>();
      CreateMap<RiwayatLainUpdateDto, RiwayatLain>();

      CreateMap<Pemeriksaan, PemeriksaanDto>()
        .ForMember(d => d.Url, opt => opt.MapFrom<PemeriksaanUrlResolver>());
      CreateMap<PemeriksaanCreateDto, Pemeriksaan>();
      CreateMap<PemeriksaanUpdateDto, Pemeriksaan>();

      CreateMap<Rencana, RencanaDto>()
        .ForMember(d => d.Url, opt => opt.MapFrom<RencanaUrlResolver>());
      CreateMap<RencanaCreateDto, Rencana>();
      CreateMap<RencanaUpdateDto, Rencana>();

      CreateMap<Resep, ResepDto>()
        .ForMember(d => d.Url, opt => opt.MapFrom<ResepUrlResolver>());
      CreateMap<ResepCreateDto, Resep>();

      CreateMap<UraianResep, UraianResepDto>();
      CreateMap<UraianResepCreateDto, UraianResep>();

      CreateMap<Diagnosis, DiagnosisDto>()
        .ForMember(d => d.Url, opt => opt.MapFrom<DiagnosisUrlResolver>());
      CreateMap<DiagnosisCreateDto, Diagnosis>();
      CreateMap<DiagnosisUpdateDto, Diagnosis>();

      CreateMap<Farmasi, FarmasiDto>();
      CreateMap<FarmasiCreateDto, Farmasi>();

      Func<TarifJualFarmasi, float> totalByDiscount = tjf =>
      {
        var kuantitas = 0;
        var jumlah = !kuantitas.Equals(0) ? tjf.HargaSatuan * kuantitas : tjf.HargaSatuan;
        var total = (tjf.FarmasiKlinik.MarginType == DiscountType.Persen)
                ? (jumlah) + (jumlah * (tjf.MarginKelas / 100))
                : jumlah + tjf.MarginKelas;

        return total;
      };

      CreateMap<TarifJualFarmasi, TarifJualFarmasiDto>()
        .ForMember(d => d.FarmasiId, opt => opt.MapFrom(s => s.FarmasiKlinik.FarmasiId))
        .ForMember(d => d.FarmasiNama, opt => opt.MapFrom(s => s.FarmasiKlinik.Farmasi.Nama))
        .ForMember(d => d.UnitBarangFarmasiId, opt => opt.MapFrom(s => s.FarmasiKlinik.UnitBarangFarmasiId))
        .ForMember(d => d.UnitBarangFarmasiNama, opt => opt.MapFrom(s => s.FarmasiKlinik.UnitBarangFarmasi.Nama))
        .ForMember(d => d.MarginType, opt => opt.MapFrom(s => s.FarmasiKlinik.MarginType))
        .ForMember(d => d.HargaJual,
          opt => opt.MapFrom(s => totalByDiscount(s)));
      CreateMap<TarifJualFarmasiCreateDto, TarifJualFarmasi>();

      CreateMap<UnitBarangFarmasi, UnitBarangFarmasiDto>();
      CreateMap<UnitBarangFarmasiCreateDto, UnitBarangFarmasi>();

      CreateMap<CommonFiltersDto, CommonFilters>();
      CreateMap<SupplierFarmasi, SupplierFarmasiDto>();
      CreateMap<SupplierFarmasiCreateDto, SupplierFarmasi>();

      CreateMap<FarmasiDibeli, FarmasiDibeliDto>();
      CreateMap<FarmasiDibeliByNameCreateDto, FarmasiDibeli>();


      CreateMap<FakturBeliFarmasi, FakturBeliFarmasiDto>();
      CreateMap<FakturBeliFarmasiCreateDto, FakturBeliFarmasi>()
        .ForMember(d => d.FarmasiDibeli, opt => opt.Ignore());

      CreateMap<StokFarmasi, StokFarmasiDto>();
      CreateMap<StokFarmasiCreateDto, StokFarmasi>();

      CreateMap<AccessViewRole, AccessViewRoleDto>()
        .ForMember(d => d.AccessViewParentId, opt => opt.MapFrom(s => s.AccessView.ParentId))
        .ForMember(d => d.AccessViewObject, opt => opt.MapFrom(s => s.AccessView.Object))
        .ForMember(d => d.AccessViewPath, opt => opt.MapFrom(s => s.AccessView.Path));
      CreateMap<AccessViewRoleCreateDto, AccessViewRole>()
        .ForMember(d => d.Ins, opt => opt.MapFrom(s => s.Ins ? 1 : 0))
        .ForMember(d => d.Upd, opt => opt.MapFrom(s => s.Upd ? 1 : 0))
        .ForMember(d => d.Del, opt => opt.MapFrom(s => s.Del ? 1 : 0))
        .ForMember(d => d.Ron, opt => opt.MapFrom(s => s.Ron ? 1 : 0));

      CreateMap<AccessView, AccessViewDto>();
      CreateMap<GudangFarmasi, GudangFarmasiDto>();

      CreateMap<FarmasiKlinik, FarmasiKlinikDto>();
      CreateMap<FarmasiKlinikCreateDto, FarmasiKlinik>();
      CreateMap<FarmasiKlinik, FarmasiKlinikCreateDto>();

      CreateMap<FarmasiKlinik, FarmasiKlinikUnitDto>();
      CreateMap<FarmasiKlinikCreateDto, FarmasiKlinik>();

      CreateMap<FakturJualFarmasi, FakturJualFarmasiDto>();
      CreateMap<FakturJualFarmasiCreateDto, FakturJualFarmasi>();

      Func<FarmasiDijual, float> totFarmDijual = fj =>
      {
        var jumlah = fj.HargaJualSat * fj.Kuantitas;
        var total = fj.TipeDisc == DiscountType.Persen ?
            jumlah + jumlah * (fj.Disc / 100) : jumlah + fj.Disc;

        return total;
      };

      CreateMap<FarmasiDijual, FarmasiDijualDto>()
        .ForMember(d => d.FarmasiId, opt => opt.MapFrom(s => s.TarifJualFarmasi.FarmasiKlinik.FarmasiId))
        .ForMember(d => d.FarmasiNama, opt => opt.MapFrom(s => s.TarifJualFarmasi.FarmasiKlinik.Farmasi.Nama))
        .ForMember(d => d.UnitBarangFarmasiId, opt => opt.MapFrom(s => s.TarifJualFarmasi.FarmasiKlinik.UnitBarangFarmasiId))
        .ForMember(d => d.UnitBarangFarmasiNama, opt => opt.MapFrom(s => s.TarifJualFarmasi.FarmasiKlinik.UnitBarangFarmasi.Nama))
        .ForMember(d => d.HargaBeliSatMax, opt => opt.MapFrom(s => s.TarifJualFarmasi.FarmasiKlinik.HargaBeliSatMax))
        .ForMember(d => d.FarmasiKlinikId, opt => opt.MapFrom(s => s.TarifJualFarmasi.FarmasiKlinik.Id))
        .ForMember(d => d.Kode, opt => opt.MapFrom(s => s.TarifJualFarmasi.FarmasiKlinik.Kode))
        .ForMember(d => d.Margin, opt => opt.MapFrom(s => s.TarifJualFarmasi.FarmasiKlinik.Margin))
        .ForMember(d => d.MarginType, opt => opt.MapFrom(s => s.TarifJualFarmasi.FarmasiKlinik.MarginType))
        .ForMember(d => d.KelasLayananId, opt => opt.MapFrom(s => s.TarifJualFarmasi.KelasLayananId))
        .ForMember(d => d.StokTotal, opt => opt.MapFrom(s => s.TarifJualFarmasi.FarmasiKlinik.StokTotal))
        .ForMember(d => d.TarifJualFarmasiId, opt => opt.MapFrom(s => s.TarifJualFarmasi.Id))
        .ForMember(d => d.MarginKelas, opt => opt.MapFrom(s => s.TarifJualFarmasi.MarginKelas))
        .ForMember(d => d.HargaSatuan, opt => opt.MapFrom(s => s.TarifJualFarmasi.HargaSatuan))

        .ForMember(d => d.Jumlah, opt => opt.MapFrom(s => s.Kuantitas * s.HargaJualSat))
        .ForMember(d => d.TipeDiscStr, opt => opt.MapFrom(s => s.TipeDisc == DiscountType.Persen ? "%" : "Rp."))
        .ForMember(d => d.Total, opt => opt.MapFrom(s => totFarmDijual(s)));
      CreateMap<FarmasiDijualCreateDto, FarmasiDijual>();

      CreateMap<User, UserDto>();
      CreateMap<UserUpdateDto, User>();

      CreateMap<JasaLayanan, JasaLayananFixedDto>()
        .ForMember(d => d.HargaSatuan, opt => opt.MapFrom(s => s.TarifJasa.FirstOrDefault().HargaSatuan))
        .ForMember(d => d.JasaRs, opt => opt.MapFrom(s => s.TarifJasa.FirstOrDefault().JasaRs))
        .ForMember(d => d.TarifJasaId, opt => opt.MapFrom(s => s.TarifJasa.FirstOrDefault().Id))
        .ForMember(d => d.JasaMedik, opt => opt.MapFrom(s => s.TarifJasa.FirstOrDefault().JasaMedik))
        .ForMember(d => d.BahanHabisPakai, opt => opt.MapFrom(s => s.TarifJasa.FirstOrDefault().BahanHabisPakai))
        .ForMember(d => d.KodeTindakBpjs, opt => opt.MapFrom(s => s.TarifJasa.FirstOrDefault().KodeTindakBpjs));

      Func<FarmasiKurang, string> getPenyebab = p =>
      {
        var penyebab = "";

        switch (p.Penyebab)
        {
          case Penyebab.Kedaluwarsa:
            penyebab = "Kedaluwarsa";
            break;
          case Penyebab.Rusak:
            penyebab = "Rusak";
            break;
          case Penyebab.Hilang:
            penyebab = "Hilang";
            break;
          case Penyebab.LainLain:
            penyebab = "Lain-lain";
            break;
        }

        return penyebab;
      };

      CreateMap<FarmasiKurang, FarmasiKurangDto>()
        .ForMember(d => d.PenyebabStr, opt => opt.MapFrom(s =>getPenyebab(s)));
      CreateMap<FarmasiKurangCreateDto, FarmasiKurang>();

      CreateMap<Penyakit, PenyakitDto>();
      CreateMap<PenyakitCreateDto, Penyakit>();

      CreateMap<Pendidikan, KeyValuePairDto>();
    }
/* 
    private static float TotalByDiscount(int hargaSatuan,
      DiscountType discountType, float disc, float kuantitas = 0)
    {
      var jumlah = !kuantitas.Equals(0) ? hargaSatuan * kuantitas : hargaSatuan;
      var total = (discountType == DiscountType.Persen)
        ? (jumlah) + (jumlah * (disc / 100))
        : jumlah + disc;

      return total;
    }
 */
  }
}
