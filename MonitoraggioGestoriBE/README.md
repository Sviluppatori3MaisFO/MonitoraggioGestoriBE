# Importazione struttura database Oracle con Entity Framework Core

Questo progetto utilizza Entity Framework Core per mappare la struttura del database Oracle.

## 🛠️ Comando per lo scaffold del DbContext

Esegui il comando seguente per generare le entità e il contesto:

```bash
dotnet ef dbcontext scaffold "User Id=flussi_fin;Password=nextam;Data Source=192.168.100.184:1521/orapsd;" Oracle.EntityFrameworkCore \
  --output-dir Data/Models \
  --context-dir Data \
  --context FlussiFinContext \
  --use-database-names \
  --table AN_MONITORAGGIO_GESTORI \
  --table AN_GESTORI \
  --table SALDI_NORMALIZZATI \
  --table IMPORTAZIONE_GESTORI \
  --table MOVIMENTI_NORMALIZZATI \
  --table MONITORAGGIO_GESTORI_IMPORTAZIONE_FLUSSI \
  --data-annotations \
  --force
```

1. In FlussiFinContext eliminare tutti i "ValueGeneratedOnAdd()" della tabella MOVIMENTI_NORMALIZZATI
   - Es.
     - entity.Property(e => e.CAUSALE)
       .ValueGeneratedOnAdd()
       .HasComment("CAUSALE OPERAZIONE");
