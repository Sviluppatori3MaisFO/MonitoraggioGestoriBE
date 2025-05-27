using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MonitoraggioGestoriBE.Data.Models;

namespace MonitoraggioGestoriBE.Data;

public partial class FlussiFinContext : DbContext
{
    public FlussiFinContext()
    {
    }

    public FlussiFinContext(DbContextOptions<FlussiFinContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AN_GESTORI> AN_GESTORIs { get; set; }

    public virtual DbSet<AN_MONITORAGGIO_GESTORI> AN_MONITORAGGIO_GESTORIs { get; set; }

    public virtual DbSet<MONITORAGGIO_GESTORI_IMPORTAZIONE_FLUSSI> MONITORAGGIO_GESTORI_IMPORTAZIONE_FLUSSIs { get; set; }

    public virtual DbSet<MOVIMENTI_NORMALIZZATI> MOVIMENTI_NORMALIZZATIs { get; set; }

    public virtual DbSet<SALDI_NORMALIZZATI> SALDI_NORMALIZZATIs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseOracle("User Id=flussi_fin;Password=nextam;Data Source=192.168.100.184:1521/orapsd;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("FLUSSI_FIN")
            .UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<AN_GESTORI>(entity =>
        {
            entity.HasKey(e => e.ID_GESTORE).HasName("PK_ID_GESTORE");

            entity.Property(e => e.DS_GESTORE_REPORT).HasComment("Descrizione breve (usata in reportistica)");
            entity.Property(e => e.DT_RIFERIMENTO_PER_QUADRATURE)
                .HasDefaultValueSql("'DT_MOVIMENTO'")
                .HasComment("indica il campo data da usare nei calcoli per le quadrature: 'DT_MOVIMENTO'=contabile  'DT_OPERAZIONE'= disponibile ");
            entity.Property(e => e.UTENTE_CREAZIONE).HasComment("ID tabella utenti GLOBAUTH");
            entity.Property(e => e.UTENTE_MODIFICA).HasComment("ID tabella utenti GLOBAUTH");
        });

        modelBuilder.Entity<AN_MONITORAGGIO_GESTORI>(entity =>
        {
            entity.HasKey(e => e.ID_MONITORAGGIO_GESTORE).HasName("PK_ID_MONITORAGGIO_GESTORE");

            entity.Property(e => e.ID_MONITORAGGIO_GESTORE)
                .ValueGeneratedOnAdd()
                .HasComment("ID Progressivo PK tabella");
            entity.Property(e => e.COMPRAVENDITA_DIVISA).HasComment("1 se il gestore è compravendita divisa 0 se non lo è");
            entity.Property(e => e.DT_ARRIVO_FLUSSI_MM_D1).HasComment("Data in cui di solito arrivano i flussi considerare una sett. da quel gg o crea day 1(crea un rage di giorni)");
            entity.Property(e => e.DT_ARRIVO_FLUSSI_MM_D2).HasComment("Data in cui di solito arrivano i flussi considerare una sett. da quel gg  o crea day 2 (crea un rage di giorni)");
            entity.Property(e => e.DT_ARRIVO_FLUSSI_SS).HasComment("GG della settimana in cui arrivano i flussi in numero del gg es. Lunedi=1 Martedi=2");
            entity.Property(e => e.DT_CREAZIONE).HasComment("DT di inizio monitoraggio ");
            entity.Property(e => e.DT_LAST_EDIT).HasComment("DT ultima modifica an");
            entity.Property(e => e.EMAIL_GESTORE_1).HasComment("Email referente gestore");
            entity.Property(e => e.EMAIL_GESTORE_2).HasComment("Email referente gestore");
            entity.Property(e => e.FG_MONITORING)
                .HasDefaultValueSql("1")
                .HasComment("Flag di monitoraggio 1 monitoriamo ancora 2 no (eliminazione logica)");
            entity.Property(e => e.ID_GESTORE).HasComment("Corrispondenza con AN_GESTORI");
            entity.Property(e => e.NOTE_GESTORE).HasComment("Note del gestore espresse in HTML");

            entity.HasOne(d => d.ID_GESTORENavigation).WithMany(p => p.AN_MONITORAGGIO_GESTORIs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("GESTORE");
        });

        modelBuilder.Entity<MONITORAGGIO_GESTORI_IMPORTAZIONE_FLUSSI>(entity =>
        {
            entity.HasKey(e => e.ID_IMPORTAZIONE_FLUSSO).HasName("PK_ID_IMPORTAZIONE_FLUSSO");

            entity.Property(e => e.DT_IMPORT_MM).HasComment("DATA_IMPORTAZIONE MENSILE");
            entity.Property(e => e.DT_IMPORT_SS).HasComment("DATA_IMPORTAZIONE SETTIMANALE");
            entity.Property(e => e.FG_IMPORTAZIONE_MM).HasComment("FG IMPORTAZIONE MENSILE se limportazione e' effettuata");
            entity.Property(e => e.FG_IMPORTAZIONE_SS).HasComment("FG IMPORTAZIONE SETTIMANALE se limportazione e' effettuata");
            entity.Property(e => e.ID_GESTORE).HasComment("ID GESTORE COLLEGATO AD AN_GESTORI");
            entity.Property(e => e.ID_IMPORTAZIONE_FLUSSO)
                .ValueGeneratedOnAdd()
                .HasComment("Index");
            entity.Property(e => e.NOTE_HTML).HasComment("NOTE Sull importazione");

            entity.HasOne(d => d.ID_GESTORENavigation).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("AN_GESTORI");

        });

        modelBuilder.Entity<MOVIMENTI_NORMALIZZATI>(entity =>
        {
            entity.HasKey(e => e.ID_MOV_NORMALIZZATO).HasName("PK_MOVIMENTI_NORM");

            entity.ToTable("MOVIMENTI_NORMALIZZATI", tb => tb.HasComment("Tutti i movimenti importati nel sistema e normalizzati secondo il formato standard"));

            
            entity.HasOne(d => d.ID_GESTORENavigation).WithMany(p => p.MOVIMENTI_NORMALIZZATIs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MOV_GESTORE");
        });

        modelBuilder.Entity<SALDI_NORMALIZZATI>(entity =>
        {
            entity.Property(e => e.ID_IMPORTAZIONE).HasComment("progressivo che raggruppa l'importazione di un gruppo di saldi");
            entity.Property(e => e.ID_TRACCIATO).HasComment("id del tracciato associato a questo saldo (AN_TRACCIATI)");
            entity.Property(e => e.STATO).HasComment("'NE'=normalizzato errore, 'N'=normalizzato, 'IE'=importazione errore 'IV'=importazione valido, 'I'=importato");
            entity.Property(e => e.TIPOLOGIA_SALDO).HasComment("'TIT' = saldo titolo , 'CC' = saldo conto corrente");

            entity.HasOne(d => d.ID_GESTORENavigation).WithMany(p => p.SALDI_NORMALIZZATIs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GESTORE_SALDI_NORM");
        });
        modelBuilder.HasSequence("S_AN_GESTORI_BANCHE");
        modelBuilder.HasSequence("S_AN_GESTORI_STORICO");
        modelBuilder.HasSequence("S_AN_RAPPORTI");
        modelBuilder.HasSequence("S_AN_RAPPORTI_CONTI");
        modelBuilder.HasSequence("S_AN_TIPI_MOV_DETT");
        modelBuilder.HasSequence("S_AN_TRACCIATI");
        modelBuilder.HasSequence("S_ERRORI_CONTABILITA");
        modelBuilder.HasSequence("S_ERRORI_NORMALIZZAZIONE");
        modelBuilder.HasSequence("S_EXPORT_MAPPIN_COMM");
        modelBuilder.HasSequence("S_LOG_IMPORTAZIONE");
        modelBuilder.HasSequence("S_MAPPING_CATEGORIE_TITOLI");
        modelBuilder.HasSequence("S_MAPPING_CAUSALI");
        modelBuilder.HasSequence("S_MAPPING_CAUSALI_NORM_ECEDIS");
        modelBuilder.HasSequence("S_MAPPING_COLONNE");
        modelBuilder.HasSequence("S_MAPPING_COLONNE_ECEDIS");
        modelBuilder.HasSequence("S_MAPPING_NORM");
        modelBuilder.HasSequence("S_MAPPING_POST_PROCESSING");
        modelBuilder.HasSequence("S_MAPPING_RIGHE_MULTIPLE");
        modelBuilder.HasSequence("S_MOV_DEFINITIVI");
        modelBuilder.HasSequence("S_MOVIMENTI_NORMALIZZATI");
        modelBuilder.HasSequence("S_NEXTAM_PARTNERS");
        modelBuilder.HasSequence("S_NORMALIZZATI_STORICO");
        modelBuilder.HasSequence("S_RAPPORTI_EXTRA_INTESTATARI");
        modelBuilder.HasSequence("S_SALDI_CC");
        modelBuilder.HasSequence("S_SALDI_CC_MAP_COLONNE");
        modelBuilder.HasSequence("S_SALDI_IMPORT_ERRORI");
        modelBuilder.HasSequence("S_SALDI_MAP_COLONNE");
        modelBuilder.HasSequence("S_SALDI_MAP_TIPOLOGIA");
        modelBuilder.HasSequence("S_SALDI_NORM_ERRORI");
        modelBuilder.HasSequence("S_SALDI_NORMALIZZATI");
        modelBuilder.HasSequence("S_SALDI_TITOLI");
        modelBuilder.HasSequence("S_SALDI_TITOLI_MAP_COLONNE");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
