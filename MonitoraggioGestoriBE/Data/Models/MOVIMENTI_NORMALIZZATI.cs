using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MonitoraggioGestoriBE.Data.Models;

/// <summary>
/// Tutti i movimenti importati nel sistema e normalizzati secondo il formato standard
/// </summary>
[Table("MOVIMENTI_NORMALIZZATI")]
public partial class MOVIMENTI_NORMALIZZATI
{
    /// <summary>
    /// ID PROGRESSIVO DEL MOVIMENTO IN FORMA NORMALIZZATA
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Precision(10)]
    public int ID_MOV_NORMALIZZATO { get; set; }

    /// <summary>
    /// CODICE UNIVOCO DELL&apos;OPERAZIONE SPECIFICO DEL GESTORE 
    /// </summary>
    [StringLength(100)]
    [Unicode(false)]
    public string CODICE_OPERAZIONE { get; set; } = null!;

    /// <summary>
    /// ID TABELLA GESTORI (per ora su DB MAIS)
    /// </summary>
    [Column(TypeName = "NUMBER")]
    public decimal ID_GESTORE { get; set; }

    /// <summary>
    /// NG=NON GESTITO, N=NORMALIZZATO, NM=NORMALIZZATO E MERGIATO, VN=DA VALIDARE PER NORMALIZZ, I=IGNORATO, VC=DA VALIDARE PER CONTABILITA , V=VALIDO PER CONTABILITA M=MOVIMENTATO IN CEDIS3 
    /// </summary>
    [StringLength(2)]
    [Unicode(false)]
    public string? STATO { get; set; }

    /// <summary>
    /// CONTO CORRENTE
    /// </summary>
    [StringLength(50)]
    [Unicode(false)]
    public string? CONTO_CORRENTE_1 { get; set; }

    /// <summary>
    /// CODICE RAPPORTO BANCARIO
    /// </summary>
    [StringLength(100)]
    [Unicode(false)]
    public string? CODICE_RAPPORTO1 { get; set; }

    /// <summary>
    /// CODICE RAPPORTO BANCARIO DESTINAZIONE (SE APPLICABILE)
    /// </summary>
    [StringLength(100)]
    [Unicode(false)]
    public string? CODICE_RAPPORTO2 { get; set; }

    /// <summary>
    /// SOCIETA INTESTATARIA DEL RAPPORTO
    /// </summary>
    [StringLength(100)]
    [Unicode(false)]
    public string? INTESTATARIO { get; set; }

    /// <summary>
    /// NUMERO OPERAZIONE (ASSEGNATO DAL GESTORE)
    /// </summary>
    [StringLength(100)]
    [Unicode(false)]
    public string? NUM_OPERAZIONE { get; set; }

    /// <summary>
    /// DATA OPERAZIONE ( o DATA MOVIMENTO)
    /// </summary>
    [Column(TypeName = "DATE")]
    public DateTime? DATA_OPERAZIONE { get; set; }

    /// <summary>
    /// DATA VALUTA
    /// </summary>
    [Column(TypeName = "DATE")]
    public DateTime? DATA_VALUTA { get; set; }

    /// <summary>
    /// TIPO OPERAZIONE (CLASSIFICAZIONE DEFINITA IN AN_CAUSALI_NORM)
    /// </summary>
    [StringLength(100)]
    [Unicode(false)]
    public string? TIPO_OPERAZIONE { get; set; }

    /// <summary>
    /// CAUSALE OPERAZIONE
    /// </summary>
    [StringLength(100)]
    [Unicode(false)]
    public string? CAUSALE { get; set; }

    /// <summary>
    /// DESCRIZIONE OPERAZIONE 
    /// </summary>
    [StringLength(500)]
    [Unicode(false)]
    public string? DESCRIZIONE { get; set; }

    /// <summary>
    /// CATEGORIA DEL TITOLO
    /// </summary>
    [StringLength(100)]
    [Unicode(false)]
    public string? TITOLO_CATEGORIA { get; set; }

    /// <summary>
    /// CODICE DEL TITOLO SECONDO L&apos;ANAGRAFICA DEL GESTORE
    /// </summary>
    [StringLength(100)]
    [Unicode(false)]
    public string? CODICE_TITOLO_INTERNO1 { get; set; }

    /// <summary>
    /// CODICE DEL TITOLO DESTINAZIONE SECONDO L&apos;ANAGRAFICA DEL GESTORE (SE APPLICABILE)
    /// </summary>
    [StringLength(100)]
    [Unicode(false)]
    public string? CODICE_TITOLO_INTERNO2 { get; set; }

    /// <summary>
    /// CODICE ISIN DEL TITOLO
    /// </summary>
    [StringLength(100)]
    [Unicode(false)]
    public string? ISIN_TITOLO1 { get; set; }

    /// <summary>
    /// CODICE ISIN DEL TITOLO DESTINAZIONE (SE APPLICABILE)
    /// </summary>
    [StringLength(100)]
    [Unicode(false)]
    public string? ISIN_TITOLO2 { get; set; }

    /// <summary>
    /// DIVISA DEL TITOLO
    /// </summary>
    [StringLength(100)]
    [Unicode(false)]
    public string? DIVISA_TITOLO1 { get; set; }

    /// <summary>
    /// DIVISA DEL TITOLO DESTINAZIONE (SE APPLICABILE)
    /// </summary>
    [StringLength(100)]
    [Unicode(false)]
    public string? DIVISA_TITOLO2 { get; set; }

    /// <summary>
    /// DESCRIZIONE DEL TITOLO
    /// </summary>
    [StringLength(200)]
    [Unicode(false)]
    public string? DESCRIZIONE_TITOLO1 { get; set; }

    /// <summary>
    /// DESCRIZIONE DEL TITOLO DESTINAZIONE (SE APPLICABILE)
    /// </summary>
    [StringLength(200)]
    [Unicode(false)]
    public string? DESCRIZIONE_TITOLO2 { get; set; }

    /// <summary>
    /// TICKER BLOOMBERG DEL TITOLO
    /// </summary>
    [StringLength(100)]
    [Unicode(false)]
    public string? TICKER_TITOLO1 { get; set; }

    /// <summary>
    /// DESCRIZIONE DEL TITOLO DESTINAZIONE (SE APPLICABILE)
    /// </summary>
    [StringLength(100)]
    [Unicode(false)]
    public string? TICKER_TITOLO2 { get; set; }

    /// <summary>
    /// DIVISA DELL&apos;OPERAZIONE
    /// </summary>
    [StringLength(100)]
    [Unicode(false)]
    public string? DIVISAMOVIMENTO { get; set; }

    /// <summary>
    /// QUANTITA TITOLO
    /// </summary>
    [StringLength(100)]
    [Unicode(false)]
    public string? QUANTITA_TITOLO1 { get; set; }

    /// <summary>
    /// QUANTITA TITOLO  DESTINAZIONE (SE APPLICABILE)
    /// </summary>
    [StringLength(100)]
    [Unicode(false)]
    public string? QUANTITA_TITOLO2 { get; set; }

    /// <summary>
    /// CAMBIO Titolo - Euro
    /// </summary>
    [StringLength(100)]
    [Unicode(false)]
    public string? CAMBIO { get; set; }

    /// <summary>
    /// PREZZO DEL TITOLO (sempre in base 1)
    /// </summary>
    [StringLength(100)]
    [Unicode(false)]
    public string? PREZZO_TITOLO1 { get; set; }

    /// <summary>
    /// PREZZO DEL TITOLO DESTINAZIONE (sempre in base 1)
    /// </summary>
    [StringLength(100)]
    [Unicode(false)]
    public string? PREZZO_TITOLO2 { get; set; }

    /// <summary>
    /// RATEO DEL TITOLO
    /// </summary>
    [StringLength(100)]
    [Unicode(false)]
    public string? RATEO_TITOLO1 { get; set; }

    /// <summary>
    /// RATEO DEL TITOLO DESTINAZIONE (SE APPLICABILE)
    /// </summary>
    [StringLength(100)]
    [Unicode(false)]
    public string? RATEO_TITOLO2 { get; set; }

    /// <summary>
    /// COMMISSIONI
    /// </summary>
    [StringLength(100)]
    [Unicode(false)]
    public string? COMMISSIONI { get; set; }

    /// <summary>
    /// SPESE FISSE
    /// </summary>
    [StringLength(100)]
    [Unicode(false)]
    public string? SPESE_FISSE { get; set; }

    /// <summary>
    /// SPESE DI BROKERAGGIO
    /// </summary>
    [StringLength(100)]
    [Unicode(false)]
    public string? SPESE_BROKER { get; set; }

    /// <summary>
    /// IMPOSTE E BOLLI
    /// </summary>
    [StringLength(100)]
    [Unicode(false)]
    public string? BOLLI_TASSE { get; set; }

    /// <summary>
    /// ALTRE SPESE
    /// </summary>
    [StringLength(100)]
    [Unicode(false)]
    public string? ALTRE_SPESE { get; set; }

    /// <summary>
    /// CONTROVALORE NETTO DEL TITOLO
    /// </summary>
    [StringLength(100)]
    [Unicode(false)]
    public string? CTV_NETTO_TITOLO1 { get; set; }

    /// <summary>
    /// CONTROVALORE NETTO DEL TITOLO DESTINAZIONE (SE APPLICABILE)
    /// </summary>
    [StringLength(100)]
    [Unicode(false)]
    public string? CTV_NETTO_TITOLO2 { get; set; }

    /// <summary>
    /// CONTROVALORE TOTALE IMPORTO
    /// </summary>
    [StringLength(100)]
    [Unicode(false)]
    public string? CTV_IMPORTO { get; set; }

    /// <summary>
    /// BROKER
    /// </summary>
    [StringLength(100)]
    [Unicode(false)]
    public string? BROKER { get; set; }

    /// <summary>
    /// EX DATE
    /// </summary>
    [Column(TypeName = "DATE")]
    public DateTime? EX_DATE { get; set; }

    /// <summary>
    /// MERCATO DI QUOTAZIONE
    /// </summary>
    [StringLength(100)]
    [Unicode(false)]
    public string? MERCATO { get; set; }

    /// <summary>
    /// CONTROVALORE NETTO IN EURO
    /// </summary>
    [StringLength(100)]
    [Unicode(false)]
    public string? CTV_NETTO_EURO { get; set; }

    /// <summary>
    /// CONTROVALORE LORDO IN EURO
    /// </summary>
    [StringLength(100)]
    [Unicode(false)]
    public string? CTV_LORDO_EURO { get; set; }

    /// <summary>
    /// CONTO CORRENTE DESTINAZIONE (SE APPLICABILE)
    /// </summary>
    [StringLength(50)]
    [Unicode(false)]
    public string? CONTO_CORRENTE_2 { get; set; }

    /// <summary>
    /// DATA DI NORMALIZZAZIONE DELLA RIGA
    /// </summary>
    [Column(TypeName = "DATE")]
    public DateTime? DATA_NORMALIZZAZIONE { get; set; }

    /// <summary>
    /// DATA IMPORTAZIONE NELLA TABELLA NORMALIZZATA
    /// </summary>
    [Column(TypeName = "DATE")]
    public DateTime DATA_IMPORTAZIONE { get; set; }

    [Column(TypeName = "DATE")]
    public DateTime? DATA_MODIFICA { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? UTENTE_MODIFICA { get; set; }

    /// <summary>
    /// tipologia di movimento da creare in ecedis3 nel caso di mapping multiplo in MAPPING_CAUSALI_NORM_ECEDIS
    /// </summary>
    [StringLength(2)]
    [Unicode(false)]
    public string? TIPO_MOV_ECEDIS { get; set; }

    /// <summary>
    /// flag &apos;S&apos; o &apos;N&apos; getito dall&apos;utente che identifica se una riga normalizzata ¿ gi¿ stata vista ma lasciata nella Gestione Flussi 
    /// </summary>
    [StringLength(1)]
    [Unicode(false)]
    public string? VISUALIZZATO { get; set; }

    /// <summary>
    /// note inserite dagli utenti in Gestione Flussi
    /// </summary>
    [StringLength(512)]
    [Unicode(false)]
    public string? NOTE { get; set; }

    /// <summary>
    /// testo da usare come descrizione del movimento inserito dall&apos;utente in Gestione Flussi
    /// </summary>
    [StringLength(512)]
    [Unicode(false)]
    public string? DESCRIZIONE_CUSTOM { get; set; }

    /// <summary>
    /// 0 = flusso settimanale ; 1 = flusso definitivo
    /// </summary>
    [Precision(4)]
    public byte TIPO_FLUSSO { get; set; }

    /// <summary>
    /// TASSA APPLICABILE AI DIVIDENDI
    /// </summary>
    [StringLength(100)]
    [Unicode(false)]
    public string? TASSA_DIVIDENDI { get; set; }

    /// <summary>
    /// TOBIN TAX
    /// </summary>
    [StringLength(100)]
    [Unicode(false)]
    public string? TOBIN_TAX { get; set; }

    /// <summary>
    /// imposta sulle transazioni finanziarie
    /// </summary>
    [StringLength(100)]
    [Unicode(false)]
    public string? IMPOSTA_MOV_FINANZA { get; set; }

    /// <summary>
    /// data contabile
    /// </summary>
    [Column(TypeName = "DATE")]
    public DateTime? DATA_CONTABILE { get; set; }

    /// <summary>
    /// Cambio Titolo - c/c
    /// </summary>
    [StringLength(100)]
    [Unicode(false)]
    public string? CAMBIO_TIT_CC { get; set; }

    /// <summary>
    /// IDENTIFICA IL NOME DEL FOGLIO NEL FILE GESTORI
    /// </summary>
    [StringLength(50)]
    [Unicode(false)]
    public string? NOME_FOGLIO { get; set; }

    /// <summary>
    /// id del tracciato associato a questo movimento (AN_TRACCIATI)
    /// </summary>
    [Precision(10)]
    public int? ID_TRACCIATO { get; set; }

    /// <summary>
    /// codice operazione dell&apos;eventuale movimento mergiato manualmente
    /// </summary>
    [StringLength(100)]
    [Unicode(false)]
    public string? CODICE_OPERAZIONE_MERGIATO { get; set; }

    [ForeignKey("ID_GESTORE")]
    [InverseProperty("MOVIMENTI_NORMALIZZATIs")]
    public virtual AN_GESTORI ID_GESTORENavigation { get; set; } = null!;
}
