using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Entities
{
    public class HistoriaClinicaDTO
    {
        public int idHistoria { get; set; }
        public string Codigo { get; set; }
        public int idPaciente { get; set; }
        public string NombrePaciente { get; set; }
        public int Edad { get; set; }
        public string Sexo { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Dni { get; set; }
        public DateTime FechaActual { get; set; }
        public string MotivoConsulta { get; set; }
        public bool AntecedentesFamiliares { get; set; }
        public string AF_Padres { get; set; }
        public string AF_Padres_Vivos { get; set; }
        public string AF_Padres_Fallecidos { get; set; }
        public string AF_Padres_Causas { get; set; }
        public string AF_Hermanos { get; set; }
        public string AF_Hermanos_Vivos { get; set; }
        public string AF_Hermanos_Fallecidos { get; set; }
        public string AF_Hermanos_Causas { get; set; }
        public string AF_Hijos { get; set; }
        public string AF_Hijos_Vivos { get; set; }
        public string AF_Hijos_Fallecidos { get; set; }
        public string AF_Hijos_Causas { get; set; }
        public bool AP_Alcohol { get; set; }
        public bool AP_tabaco { get; set; }
        public bool AP_Drogas { get; set; }
        public bool AP_Transfusiones { get; set; }
        public bool P_Diabetis { get; set; }
        public bool P_Hipertension { get; set; }
        public bool P_Tuberculosis { get; set; }
        public bool P_Hepatitis { get; set; }
        public bool P_Vih { get; set; }
        public bool P_Sifilis { get; set; }
        public bool P_Asma { get; set; }
        public bool P_Gota { get; set; }
        public bool P_Hipercolesterolemia { get; set; }
        public bool P_Migraña { get; set; }
        public bool P_EnfVascular { get; set; }
        public string P_EnfVascular_Cuando { get; set; }
        public bool P_Infartos { get; set; }
        public string P_Infartos_Cuando { get; set; }
        public bool P_Varices { get; set; }
        public string P_Varices_Lugar { get; set; }
        public bool P_Anticoagulantes { get; set; }
        public string P_Anticoagulantes_Dosis { get; set; }
        public bool P_Aspirina { get; set; }
        public string P_Aspirina_Dosis { get; set; }
        public bool P_Anticonceptivos { get; set; }
        public string P_Anticonceptivos_Dosis { get; set; }
        public bool P_Colageno { get; set; }
        public string P_Colageno_Cual { get; set; }
        public bool P_Tiroides { get; set; }
        public string P_Tiroides_Medicacion { get; set; }
        public bool P_Cancer { get; set; }
        public string P_Cancer_Lugar { get; set; }
        public bool P_Mentales { get; set; }
        public string P_Mentales_Medicacion { get; set; }
        public bool P_Convulsiones { get; set; }
        public string P_Convulsiones_Medicacion { get; set; }
        public DateTime FechaUltimaRegla { get; set; }
        public string Embarazos { get; set; }
        public string AntecedentesQx { get; set; }
        public string Alergias { get; set; }
        public string MedicacionPaciente { get; set; }
        public string Otros { get; set; }
        public string EF_ImpresionGeneral { get; set; }
        public string EF_SignosVitales_FC { get; set; }
        public string EF_SignosVitales_TA { get; set; }
        public string EF_SignosVitales_FR { get; set; }
        public string EF_SignosVitales_Pulso { get; set; }
        public string EF_TAuxiliar { get; set; }
        public decimal EF_PesoHabitual { get; set; }
        public decimal EF_PesoActual { get; set; }
        public decimal EF_Talla { get; set; }
        public string EF_IMC { get; set; }
        public bool Nutricion { get; set; }
        public string N_Calorias_Dia { get; set; }
        public string N_NroVecesAlimentacion { get; set; }
        public string N_PorcentajeGrasa { get; set; }
        public string N_DietaRegular { get; set; }
        public string N_PreferenciasAlimentarias { get; set; }
        public string N_Objetivo { get; set; }
        public string ExamenComplementario { get; set; }
        public string OtrosEstudios { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int UsuarioCreacion { get; set; }
        public int UsuarioModificacion { get; set; }
        public bool Estado { get; set; }
        public string lista_Archivos { get; set; }        
        public List<HistoriaClinica_ArchivosDTO> oListaArchivos = new List<HistoriaClinica_ArchivosDTO>();
    }
}
