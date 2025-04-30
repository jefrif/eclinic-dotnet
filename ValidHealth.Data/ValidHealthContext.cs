// using System.IO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ValidHealth.Domain.Entities;
// using Microsoft.EntityFrameworkCore.Metadata;
// using Microsoft.Extensions.Configuration;

namespace ValidHealth.Data
{
  public class ValidHealthContext : IdentityDbContext<User, Role, long>
  {
    public ValidHealthContext(DbContextOptions<ValidHealthContext> options)
      : base(options)
    {
      // jsonset = new FileInfo(@"appsettings.json").CreationTime.Millisecond;
    }

    // public readonly int jsonset;

    /*
      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
        if (!optionsBuilder.IsConfigured)
        {
          string KlinikConnection =
            "Server = GEVREE; User Id = sa; Password = Zer02#G;Database=ValidHealth;Trusted_Connection=False;";
          optionsBuilder.UseSqlServer(KlinikConnection,
            b => b.UseRowNumberForPaging());

        }
      }
    */

    public DbSet<Dokter> Dokter { get; set; }
    public DbSet<PesertaAsuransi> PesertaAsuransi { get; set; }
    public DbSet<Anggota> Anggota { get; set; }
    public DbSet<Klinik> Klinik { get; set; }
    public DbSet<KonversiSatuan> KonversiSatuan { get; set; }
    public DbSet<Asuransi> Asuransi { get; set; }
    public DbSet<AsuransiKlinik> AsuransiKlinik { get; set; }
    public DbSet<BahanRacikanDijual> BahanRacikanDijual { get; set; }
    public DbSet<BridgingAsuransi> BridgingAsuransi { get; set; }
    public DbSet<JenisBridging> JenisBridging { get; set; }
    public DbSet<PelayananRj> PelayananRj { get; set; }
    public DbSet<Pasien> Pasien { get; set; }
    public DbSet<Provinsi> Provinsi { get; set; }
    public DbSet<KabKota> KabKota { get; set; }
    public DbSet<Kecamatan> Kecamatan { get; set; }
    public DbSet<Kelurahan> Kelurahan { get; set; }
    public DbSet<RekamMedis> RekamMedis { get; set; }
    public DbSet<Subjektif> Subjektif { get; set; }
    public DbSet<Objektif> Objektif { get; set; }
    public DbSet<Assessment> Assessment { get; set; }
    public DbSet<Plan> Plan { get; set; }
    public DbSet<InstansiPerujuk> InstansiPerujuk { get; set; }
    public DbSet<Rujukan> Rujukan { get; set; }
    public DbSet<Poliklinik> Poliklinik { get; set; }
    public DbSet<UnitKlinik> UnitKlinik { get; set; }
    public DbSet<PemasokFarmasi> PemasokFarmasi { get; set; }
    public DbSet<DokterUnitKlinik> DokterUnitKlinik { get; set; }
    public DbSet<JasaLayanan> JasaLayanan { get; set; }
    public DbSet<TarifJasa> TarifJasa { get; set; }
    public DbSet<KelasLayanan> KelasLayanan { get; set; }
    public DbSet<UnitBarangJasa> UnitBarangJasa { get; set; }
    public DbSet<JasaLayananRj> JasaLayananRj { get; set; }
    public DbSet<KelasRawatInap> KelasRawatInap { get; set; }
    public DbSet<KeluhanUtama> KeluhanUtama { get; set; }
    public DbSet<RiwayatPenyakit> RiwayatPenyakit { get; set; }
    public DbSet<RiwayatLain> RiwayatLain { get; set; }
    public DbSet<RawatInap> RawatInap { get; set; }
    public DbSet<RuangInap> RuangInap { get; set; }
    public DbSet<Pemeriksaan> Pemeriksaan { get; set; }
    public DbSet<Diagnosis> Diagnosis { get; set; }
    public DbSet<Rencana> Rencana { get; set; }
    public DbSet<ObatRacikan> ObatRacikan { get; set; }
    public DbSet<Resep> Resep { get; set; }
    public DbSet<UraianResep> UraianResep { get; set; }
    public DbSet<Farmasi> Farmasi { get; set; }
    public DbSet<FarmasiKlinik> FarmasiKlinik { get; set; }
    public DbSet<TarifJualFarmasi> TarifJualFarmasi { get; set; }
    public DbSet<UnitBarangFarmasi> UnitBarangFarmasi { get; set; }
    public DbSet<SupplierFarmasi> SupplierFarmasi { get; set; }
    public DbSet<FarmasiDibeli> FarmasiDibeli { get; set; }
    public DbSet<FakturBeliFarmasi> FakturBeliFarmasi { get; set; }
    public DbSet<GudangFarmasi> GudangFarmasi { get; set; }
    public DbSet<GudangFarmasiUser> GudangFarmasiUser { get; set; }
    public DbSet<BahanRacikanDitambah> BahanRacikanDitambah { get; set; }
    public DbSet<FarmasiKurang> FarmasiKurang { get; set; }
    public DbSet<StokFarmasi> StockFarmasi { get; set; }
    public DbSet<AccessViewRole> AccessViewRole { get; set; }
    public DbSet<AccessView> AccessView { get; set; }
    public DbSet<AlamatPelengkap> AlamatPelengkap { get; set; }
    public DbSet<FarmasiDijual> FarmasiDijual { get; set; }
    public DbSet<StokFarmasiDijual> StokFarmasiDijual { get; set; }
    public DbSet<FakturJualFarmasi> FakturJualFarmasi { get; set; }
    public DbSet<Rekening> Rekening { get; set; }
    public DbSet<Penyakit> Penyakit { get; set; }
    public DbSet<SetBlud> SetBlud { get; set; }
    public DbSet<TranskLayanRj> TranskLayanRj { get; set; }
    public DbSet<TranskBeliFarms> TranskBeliFarms { get; set; }
    public DbSet<TranskJualFarms> TranskJualFarms { get; set; }
    public DbSet<KlaimAsuransi> KlaimAsuransi { get; set; }
    public DbSet<TranskJualFarmsKlaim> TranskJualFarmsKlaim { get; set; }
    public DbSet<Dictionary> Dictionary { get; set; }
    public DbSet<ReturFarmasi> ReturFarmasi { get; set; }
    public DbSet<MutasiStokGudang> MutasiStokGudang { get; set; }
    public DbSet<Pendidikan> Pendidikan { get; set; }
    public DbSet<JenisFaskes> JenisFaskes { get; set; }
    public DbSet<JenisPemilik> JenisPemilik { get; set; }
    public DbSet<JenisAkreditasi> JenisAkreditasi { get; set; }
    public DbSet<AkreditasiFaskes> AkreditasiFaskes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      base.OnConfiguring(optionsBuilder);

      optionsBuilder.ConfigureWarnings(opt => opt.Default(WarningBehavior.Ignore));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      //foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
      //{
      //    relationship.DeleteBehavior = DeleteBehavior.Restrict;
      //}

      modelBuilder.Entity<AlamatPelengkap>(entity =>
      {
        entity.HasOne(e => e.Provinsi)
          .WithMany(p => p.AlamatPelengkaps)
          .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(e => e.KabKota)
          .WithMany(p => p.AlamatPelengkaps)
          .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(e => e.Kecamatan)
          .WithMany(p => p.AlamatPelengkaps)
          .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(e => e.Kelurahan)
          .WithMany(p => p.AlamatPelengkaps)
          .OnDelete(DeleteBehavior.Restrict);
      });

      modelBuilder.Entity<Anggota>(entity =>
      {
        entity.HasKey(e => new { e.Id });

        entity.Property(e => e.Id)
          .ValueGeneratedOnAdd();
/* 
        entity.Property(e => e.Nik).IsRequired();

        entity.HasIndex(e => e.Nik).IsUnique();
 */
      });

      modelBuilder.Entity<DokterUnitKlinik>(entity =>
      {
        entity.HasKey(e => new { e.DokterId, e.UnitKlinikId });

        entity.HasOne(p => p.Dokter)/* .WithOne() */;
        entity.HasOne(p => p.UnitKlinik)/* .WithOne() */;
      });

      modelBuilder.Entity<PemasokFarmasi>(entity =>
      {
        entity.HasKey(e => new { e.KlinikPenerimaId, e.KlinikPemasokId });

        entity.HasOne<Klinik>(p => p.KlinikPenerima).WithMany()
            .HasForeignKey(a => a.KlinikPenerimaId).OnDelete(DeleteBehavior.Restrict);
        entity.HasOne<Klinik>(p => p.KlinikPemasok).WithMany()
            .HasForeignKey(a => a.KlinikPemasokId).OnDelete(DeleteBehavior.Restrict);
      });

      modelBuilder.Entity<Pasien>(entity =>
      {
        // entity.HasOne(e => e.Anggota)
        //   .WithMany(a => a.Pasiens)
        //   .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(e => e.Klinik)
          .WithMany(k => k.Pasien)
          .OnDelete(DeleteBehavior.Restrict);
      });

      modelBuilder.Entity<PelayananRj>(entity =>
      {
        entity.HasOne(p => p.DokterUnitKlinik)
          .WithMany(/* p => p.PelayananRjs */)
          .HasForeignKey(p => new { p.DokterId, p.UnitKlinikId });

        entity.HasOne(p => p.Penanggung);
        // .WithMany(a => a.PelayananRj)
        // .HasForeignKey(p => p.PenanggungId);
      });

      modelBuilder.Entity<PesertaAsuransi>()
        .HasIndex(p => new { p.AsuransiId, p.PasienId });

      modelBuilder.Entity<Klinik>(entity =>
      {
        entity.Property(e => e.Nama)
          .IsRequired();
      });

      modelBuilder.Entity<Penyakit>()
        .HasIndex(p => p.KodeIcd)
        .IsUnique();

      modelBuilder.Entity<Provinsi>()
        .Property(p => p.Id)
        .ValueGeneratedNever();

      modelBuilder.Entity<KabKota>()
        .Property(k => k.Id)
        .ValueGeneratedNever();

      modelBuilder.Entity<Kecamatan>()
        .Property(k => k.Id)
        .ValueGeneratedNever();

      modelBuilder.Entity<Kelurahan>()
        .Property(k => k.Id)
        .ValueGeneratedNever();
/* 
      modelBuilder.Entity<JasaLayananRj>(entity =>
      {
        entity.HasKey(e => new { e.PelayananRjId, e.TarifJasaId });
      });
 */
      modelBuilder.Entity<Klinik>(entity =>
      {
        entity.HasMany(e => e.Poliklinik)
          .WithOne(e => e.Klinik)
          .OnDelete(DeleteBehavior.Restrict);
      });

      modelBuilder.Entity<FakturBeliFarmasi>(entity =>
      {
        entity.HasMany(f => f.FarmasiDibeli)
          .WithOne(f => f.FakturBeliFarmasi).IsRequired();

        entity.HasOne<FakturJualFarmasi>().WithMany()
          .HasForeignKey(a => a.FakturJualFarmasiId).OnDelete(DeleteBehavior.Restrict);

        entity.HasOne<GudangFarmasi>().WithMany()
          .HasForeignKey(a => a.GudangFarmasiId).OnDelete(DeleteBehavior.Restrict);
      });

      //modelBuilder.Entity<StokFarmasi>()
      //  .HasAlternateKey(e => new { e.KlinikId, e.FarmasiId, e.UnitBarangFarmasiId });

      modelBuilder.Entity<StokFarmasi>(entity =>
      {
        entity.HasOne(e => e.FarmasiKlinik)
          .WithMany(e => e.StokFarmasi).OnDelete(DeleteBehavior.Restrict);

        entity.HasOne<GudangFarmasi>().WithMany()
          .HasForeignKey(a => a.GudangFarmasiId).OnDelete(DeleteBehavior.Restrict);
      });

      modelBuilder.Entity<SupplierFarmasi>()
        .HasMany(e => e.FakturBeliFarmasi)
        .WithOne(e => e.SupplierFarmasi)
        .OnDelete(DeleteBehavior.Restrict);

      modelBuilder.Entity<AccessViewRole>()
        .HasKey(e => new { e.RoleId, e.AccessViewId });

      modelBuilder.Entity<AccessView>(entity =>
      {
        entity.Property(e => e.Id)
          .ValueGeneratedNever();
      });

      modelBuilder.Entity<Dictionary>(entity =>
      {
        entity.HasIndex(a => a.Id).IsUnique();
        entity.Property(e => e.Id).ValueGeneratedNever();
      });

      modelBuilder.Entity<StokFarmasiDijual>()
        .HasKey(e => new { e.StokFarmasiId, e.FarmasiDijualId });

      modelBuilder.Entity<FakturJualFarmasi>(entity => {
        entity.HasOne(e => e.Klinik)
          .WithMany(e => e.FakturJualFarmasi)
          .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne<UnitKlinik>().WithMany()
          .HasForeignKey(a => a.UnitKlinikId).OnDelete(DeleteBehavior.Restrict);

        entity.HasOne<RuangInap>().WithMany()
          .HasForeignKey(a => a.RuangInapId).OnDelete(DeleteBehavior.Restrict);

        entity.HasOne<GudangFarmasi>().WithMany()
          .HasForeignKey(a => a.GudangFarmasiId).OnDelete(DeleteBehavior.Restrict);
      });

      modelBuilder.Entity<ReturFarmasi>(entity => {
        entity.HasKey(e => new { e.Id });
        entity.HasIndex(a => a.Id).IsUnique();
        entity.HasOne(e => e.FakturJualFarmasi).WithMany()
          .OnDelete(DeleteBehavior.Restrict);
      });

      modelBuilder.Entity<Pendidikan>(entity => {
        entity.HasKey(e => new { e.Id });
        entity.HasIndex(a => a.Id).IsUnique();
        entity.HasMany<Anggota>().WithOne().HasForeignKey(a => a.PendidikanId)
            .OnDelete(DeleteBehavior.SetNull);
      });

      modelBuilder.Entity<Pemeriksaan>(entity => {
        entity.HasOne<PelayananRj>().WithMany()
          .HasForeignKey(a => a.RujukanRjId).OnDelete(DeleteBehavior.NoAction);
        // ef (.Net Core 2.1) only:
          // .HasForeignKey(a => a.RujukanRjId).OnDelete(DeleteBehavior.SetNull);
      });

      modelBuilder.Entity<RekamMedis>(entity => {
        entity.HasOne<PelayananRj>().WithMany()
          .HasForeignKey(a => a.PelayananRjId).OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<Objektif>(entity => {
        entity.HasOne<PelayananRj>().WithMany()
          .HasForeignKey(a => a.PelayananRjId).OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<SetBlud>(entity =>
      {
        entity.HasOne<Klinik>().WithMany()
          .HasForeignKey(a => a.KlinikId).OnDelete(DeleteBehavior.Cascade);

        entity.HasIndex(a => a.KlinikId).IsUnique();
      });

      modelBuilder.Entity<MutasiStokGudang>(entity =>
      {
        entity.HasMany<BarangDimutasi>(a => a.BarangDimutasis).WithOne()
            .HasForeignKey(a => a.MutasiStokGudangId).OnDelete(DeleteBehavior.Cascade);

        entity.HasOne<GudangFarmasi>().WithMany()
            .HasForeignKey(a => a.FromGudangId).OnDelete(DeleteBehavior.Restrict);

        entity.HasOne<GudangFarmasi>().WithMany()
            .HasForeignKey(a => a.ToGudangId).OnDelete(DeleteBehavior.Restrict);

        entity.HasIndex(a => a.Id).IsUnique();
      });

      modelBuilder.Entity<BarangDimutasi>(entity =>
      {
        entity.HasMany<StokDimutasi>(a => a.StokDimutasis).WithOne()
            .HasForeignKey(a => a.BarangDimutasiId).OnDelete(DeleteBehavior.Cascade);

        entity.HasOne<FarmasiKlinik>().WithMany()
            .HasForeignKey(a => a.FarmasiKlinikId).OnDelete(DeleteBehavior.Restrict);

        entity.HasIndex(a => a.Id).IsUnique();
      });

      modelBuilder.Entity<StokDimutasi>(entity =>
      {
        entity.HasOne<StokFarmasi>().WithMany()
            .HasForeignKey(a => a.StokFarmasiId).OnDelete(DeleteBehavior.Restrict);

        entity.HasOne<StokFarmasi>(a => a.StokFarmasiSend).WithMany()
            .HasForeignKey(a => a.StokFarmasiSendId).OnDelete(DeleteBehavior.Restrict);

        entity.HasIndex(a => a.Id).IsUnique();
      });

      modelBuilder.Entity<GudangFarmasi>(entity =>
      {
        entity.HasOne<Klinik>().WithMany()
          .HasForeignKey(a => a.KlinikId).OnDelete(DeleteBehavior.Restrict);

        entity.HasIndex(a => a.Id).IsUnique();
      });

      modelBuilder.Entity<GudangFarmasiUser>(entity =>
      {
        entity.HasOne<User>().WithMany()
          .HasForeignKey(a => a.UserId).OnDelete(DeleteBehavior.Cascade);

        entity.HasOne<GudangFarmasi>().WithMany()
          .HasForeignKey(a => a.GudangFarmasiId).OnDelete(DeleteBehavior.Cascade);

        entity.HasKey(e => new { e.UserId, e.GudangFarmasiId });
      });

      modelBuilder.Entity<BahanRacikanDitambah>(entity =>
      {
        entity.HasOne<FarmasiDibeli>().WithMany()
          .HasForeignKey(a => a.FarmasiDibeliId).OnDelete(DeleteBehavior.Cascade);

        entity.HasOne<StokFarmasi>().WithMany()
          .HasForeignKey(a => a.StokFarmasiId).OnDelete(DeleteBehavior.Restrict);

        entity.HasKey(e => new { e.FarmasiDibeliId, e.StokFarmasiId });
      });

      modelBuilder.Entity<KlaimAsuransi>(entity =>
      {
        // entity.HasMany(a => a.TranskKlaims);

        entity.HasIndex(a => a.Id).IsUnique();

        entity.HasOne<TranskLayanRj>().WithMany()
            .HasForeignKey(a => a.TranskLayanRjId).OnDelete(DeleteBehavior.Restrict);
        entity.HasOne<AsuransiKlinik>().WithMany()
            .HasForeignKey(a => a.AsuransiKlinikId).OnDelete(DeleteBehavior.Restrict);
      });

      modelBuilder.Entity<TranskJualFarmsKlaim>(entity =>
      {
        entity.HasOne<KlaimAsuransi>().WithMany(a => a.TranskJualFarmsKlaims)
            .HasForeignKey(a => a.KlaimAsuransiId).OnDelete(DeleteBehavior.Cascade);

        entity.HasOne<TranskJualFarms>().WithMany()
            .HasForeignKey(a => a.TranskJualFarmsId).OnDelete(DeleteBehavior.Cascade);

        entity.HasKey(e => new { e.TranskJualFarmsId, e.KlaimAsuransiId });
      });

/* 
      modelBuilder.Entity<BridgingAsuransi>()
        .HasOne(e => e.AsuransiKlinik)
        .WithMany(e => e.BridgingAsuransi)
        .OnDelete(DeleteBehavior.Restrict);
 */
      modelBuilder.Entity<AsuransiKlinik>()
        .HasMany(e => e.BridgingAsuransi);
        // .WithOne(e => e.AsuransiKlinik)    // self referencing loop when post
        // .OnDelete(DeleteBehavior.Restrict);
/* 
      modelBuilder.Entity<BridgingAsuransi>(entity =>
      {
        entity
          .HasOne(e => e.JenisBridging);
          // .WithMany(e => e.BridgingAsuransi)
          // .OnDelete(DeleteBehavior.Restrict);
      });

      modelBuilder.Entity<FarmasiDijual>(entity =>
      {
        entity.HasMany<BahanRacikanDijual>(e => e.BahanRacikanDijual).WithOne();
      });
 */
      modelBuilder.Entity<BahanRacikanDijual>(entity =>
      {
        // WARNING: Commented below are for ef migration execution only.
        //     Cause error when SaveChanges in FarmasiJualControllers
/* 
        entity.HasOne<FarmasiDijual>().WithMany()
          .HasForeignKey(a => a.FarmasiDijualId).OnDelete(DeleteBehavior.Cascade);

        entity.HasOne<StokFarmasi>().WithMany()
          .HasForeignKey(a => a.StokFarmasiId).OnDelete(DeleteBehavior.Restrict);
 */
        entity.HasKey(e => new { e.StokFarmasiId, e.FarmasiDijualId });
      });

      modelBuilder.Entity<KonversiSatuan>(entity =>
      {
        // WARNING: Commented below are for ef migration execution only.
        //     Cause error when SaveChanges in FarmasiJualControllers

        // entity.HasOne<FarmasiDijual>().WithMany()
        //   .HasForeignKey(a => a.FarmasiDijualId).OnDelete(DeleteBehavior.Cascade);

        entity.HasOne<FarmasiDibeli>(a => a.FarmasiDibeli).WithMany()
          .HasForeignKey(a => a.FarmasiDibeliId).OnDelete(DeleteBehavior.Cascade);

        entity.HasOne<StokFarmasi>(a => a.StokFarmasi).WithMany()
          .HasForeignKey(a => a.StokFarmasiId).OnDelete(DeleteBehavior.Restrict);

        entity.HasKey(e => new { e.StokFarmasiId, e.FarmasiDibeliId });
      });

      modelBuilder.Entity<FarmasiKurang>(entity =>
      {
        // WARNING: Commented below are for ef migration execution only.
        //     Cause error when SaveChanges in FarmasiJualControllers

        // entity.HasOne<FarmasiDijual>().WithMany()
        //   .HasForeignKey(a => a.FarmasiDijualId).OnDelete(DeleteBehavior.Cascade);

        entity.HasOne<StokFarmasi>().WithMany()
          .HasForeignKey(a => a.StokFarmasiId).OnDelete(DeleteBehavior.Restrict);

        entity.HasIndex(a => a.Id).IsUnique();
        entity.HasKey(e => new { e.Id });
      });

      modelBuilder.Entity<TarifJualFarmasi>()
        .HasIndex(e => new { e.FarmasiKlinikId, e.KelasLayananId })
        .IsUnique();

      modelBuilder.Entity<Rekening>()
        .Property(e => e.Id)
        .ValueGeneratedNever();

      modelBuilder.Entity<AkreditasiFaskes>(entity => {
        entity.HasKey(e => new { e.Id });
        entity.HasIndex(a => a.Id).IsUnique();
        entity.HasIndex(a => new {a.JenisFaskesId, a.JenisAkreditasiId}).IsUnique();
        entity.HasMany<Klinik>().WithOne().HasForeignKey(a => a.AkreditasiFaskesId)
            .OnDelete(DeleteBehavior.SetNull);
      });

      modelBuilder.Entity<JenisAkreditasi>(entity => {
        entity.HasKey(e => new { e.Id });
        entity.HasIndex(a => a.Id).IsUnique();
        entity.HasMany<AkreditasiFaskes>().WithOne().HasForeignKey(a => a.JenisAkreditasiId)
            .OnDelete(DeleteBehavior.Restrict);
      });

      modelBuilder.Entity<JenisFaskes>(entity => {
        entity.HasKey(e => new { e.Id });
        entity.HasIndex(a => a.Id).IsUnique();
        entity.HasMany<AkreditasiFaskes>().WithOne().HasForeignKey(a => a.JenisFaskesId)
            .OnDelete(DeleteBehavior.Restrict);
      });

      modelBuilder.Entity<JenisPemilik>(entity => {
        entity.HasKey(e => new { e.Id });
        entity.HasIndex(a => a.Id).IsUnique();
        entity.HasMany<Klinik>().WithOne().HasForeignKey(a => a.JenisPemilikId)
            .OnDelete(DeleteBehavior.SetNull);
      });

      modelBuilder.Entity<User>().ToTable("User");
      modelBuilder.Entity<Role>().ToTable("Role");
      modelBuilder.Entity<IdentityUserClaim<long>>().ToTable("UserClaim");
      modelBuilder.Entity<IdentityUserRole<long>>().ToTable("UserRole");
      modelBuilder.Entity<IdentityUserLogin<long>>().ToTable("UserLogin");
      modelBuilder.Entity<IdentityRoleClaim<long>>().ToTable("RoleClaim");
      modelBuilder.Entity<IdentityUserToken<long>>().ToTable("UserToken");
    }
    public void CopyProperties<T>(T source, T destination)
    {
      var props = source.GetType().GetProperties();
      foreach (var prop in props)
      {
        prop.SetValue(destination, prop.GetValue(source));
      }
    }
  }
}
