using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using SistemaDermoSalud.Entities;

namespace SistemaDermoSalud.DataAccess
{
    public class HistoriaClinicaDAO
    {
        public ResultDTO<HistoriaClinicaDTO> ListarTodo(SqlConnection cn = null)
        {
            ResultDTO<HistoriaClinicaDTO> oResultDTO = new ResultDTO<HistoriaClinicaDTO>();
            oResultDTO.ListaResultado = new List<HistoriaClinicaDTO>();
            using ((cn == null ? cn = new Conexion().conectar() : cn))
            {
                try
                {
                    if (cn.State == ConnectionState.Closed) { cn.Open(); }
                    SqlDataAdapter da = new SqlDataAdapter("SP_HistoriaClinica_ListarTodo", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        HistoriaClinicaDTO oHistoriaClinicaDTO = new HistoriaClinicaDTO();
                        oHistoriaClinicaDTO.idHistoria = Convert.ToInt32(dr["idHistoria"] == null ? 0 : Convert.ToInt32(dr["idHistoria"].ToString()));
                        oHistoriaClinicaDTO.Codigo = dr["Codigo"] == null ? "" : dr["Codigo"].ToString();
                        oHistoriaClinicaDTO.idPaciente = Convert.ToInt32(dr["idPaciente"] == null ? 0 : Convert.ToInt32(dr["idPaciente"].ToString()));
                        oHistoriaClinicaDTO.NombrePaciente = dr["NombrePaciente"] == null ? "" : dr["NombrePaciente"].ToString();
                        oHistoriaClinicaDTO.Dni = dr["Dni"] == null ? "" : dr["Dni"].ToString();
                        oHistoriaClinicaDTO.FechaNacimiento = Convert.ToDateTime(dr["FechaNacimiento"].ToString());
                        oHistoriaClinicaDTO.Edad = Convert.ToInt32(dr["Edad"] == null ? 0 : Convert.ToInt32(dr["Edad"].ToString()));
                        oHistoriaClinicaDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oHistoriaClinicaDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oHistoriaClinicaDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioCreacion"].ToString()));
                        oHistoriaClinicaDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"] == null ? 0 : Convert.ToInt32(dr["UsuarioModificacion"].ToString()));
                        oHistoriaClinicaDTO.Estado = Convert.ToBoolean(dr["Estado"] == null ? false : Convert.ToBoolean(dr["Estado"].ToString()));
                        oResultDTO.ListaResultado.Add(oHistoriaClinicaDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<HistoriaClinicaDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<HistoriaClinicaDTO> ListarxID(int idHistoria)
        {
            ResultDTO<HistoriaClinicaDTO> oResultDTO = new ResultDTO<HistoriaClinicaDTO>();
            oResultDTO.ListaResultado = new List<HistoriaClinicaDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_HistoriaClinica_ListarxID", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idHistoria", idHistoria);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        HistoriaClinicaDTO oHistoriaClinicaDTO = new HistoriaClinicaDTO();
                        oHistoriaClinicaDTO.idHistoria = Convert.ToInt32(dr["idHistoria"].ToString());
                        oHistoriaClinicaDTO.Codigo = dr["Codigo"].ToString();
                        oHistoriaClinicaDTO.idPaciente = Convert.ToInt32(dr["idPaciente"].ToString());
                        oHistoriaClinicaDTO.NombrePaciente = dr["NombrePaciente"].ToString();
                        oHistoriaClinicaDTO.Edad = Convert.ToInt32(dr["Edad"].ToString());
                        oHistoriaClinicaDTO.Sexo = dr["Sexo"].ToString();
                        oHistoriaClinicaDTO.FechaNacimiento = Convert.ToDateTime(dr["FechaNacimiento"].ToString());
                        oHistoriaClinicaDTO.Dni = dr["Dni"].ToString();
                        oHistoriaClinicaDTO.FechaActual = Convert.ToDateTime(dr["FechaActual"].ToString());
                        //oHistoriaClinicaDTO.MotivoConsulta = dr["MotivoConsulta"].ToString();
                        oHistoriaClinicaDTO.AntecedentesFamiliares = Convert.ToBoolean(dr["AntecedentesFamiliares"].ToString());
                        oHistoriaClinicaDTO.AF_Padres = dr["AF_Padres"].ToString();
                        oHistoriaClinicaDTO.AF_Padres_Vivos = dr["AF_Padres_Vivos"].ToString();
                        oHistoriaClinicaDTO.AF_Padres_Fallecidos = dr["AF_Padres_Fallecidos"].ToString();
                        oHistoriaClinicaDTO.AF_Padres_Causas = dr["AF_Padres_Causas"].ToString();
                        oHistoriaClinicaDTO.AF_Hermanos = dr["AF_Hermanos"].ToString();
                        oHistoriaClinicaDTO.AF_Hermanos_Vivos = dr["AF_Hermanos_Vivos"].ToString();
                        oHistoriaClinicaDTO.AF_Hermanos_Fallecidos = dr["AF_Hermanos_Fallecidos"].ToString();
                        oHistoriaClinicaDTO.AF_Hermanos_Causas = dr["AF_Hermanos_Causas"].ToString();
                        oHistoriaClinicaDTO.AF_Hijos = dr["AF_Hijos"].ToString();
                        oHistoriaClinicaDTO.AF_Hijos_Vivos = dr["AF_Hijos_Vivos"].ToString();
                        oHistoriaClinicaDTO.AF_Hijos_Fallecidos = dr["AF_Hijos_Fallecidos"].ToString();
                        oHistoriaClinicaDTO.AF_Hijos_Causas = dr["AF_Hijos_Causas"].ToString();
                        oHistoriaClinicaDTO.AP_Alcohol = Convert.ToBoolean(dr["AP_Alcohol"].ToString());
                        oHistoriaClinicaDTO.AP_tabaco = Convert.ToBoolean(dr["AP_tabaco"].ToString());
                        oHistoriaClinicaDTO.AP_Drogas = Convert.ToBoolean(dr["AP_Drogas"].ToString());
                        oHistoriaClinicaDTO.AP_Transfusiones = Convert.ToBoolean(dr["AP_Transfusiones"].ToString());
                        oHistoriaClinicaDTO.P_Diabetis = Convert.ToBoolean(dr["P_Diabetis"].ToString());
                        oHistoriaClinicaDTO.P_Hipertension = Convert.ToBoolean(dr["P_Hipertension"].ToString());
                        oHistoriaClinicaDTO.P_Tuberculosis = Convert.ToBoolean(dr["P_Tuberculosis"].ToString());
                        oHistoriaClinicaDTO.P_Hepatitis = Convert.ToBoolean(dr["P_Hepatitis"].ToString());
                        oHistoriaClinicaDTO.P_Vih = Convert.ToBoolean(dr["P_Vih"].ToString());
                        oHistoriaClinicaDTO.P_Sifilis = Convert.ToBoolean(dr["P_Sifilis"].ToString());
                        oHistoriaClinicaDTO.P_Asma = Convert.ToBoolean(dr["P_Asma"].ToString());
                        oHistoriaClinicaDTO.P_Gota = Convert.ToBoolean(dr["P_Gota"].ToString());
                        oHistoriaClinicaDTO.P_Hipercolesterolemia = Convert.ToBoolean(dr["P_Hipercolesterolemia"].ToString());
                        oHistoriaClinicaDTO.P_Migraña = Convert.ToBoolean(dr["P_Migraña"].ToString());
                        oHistoriaClinicaDTO.P_EnfVascular = Convert.ToBoolean(dr["P_EnfVascular"].ToString());
                        oHistoriaClinicaDTO.P_EnfVascular_Cuando = dr["P_EnfVascular_Cuando"].ToString();
                        oHistoriaClinicaDTO.P_Infartos = Convert.ToBoolean(dr["P_Infartos"].ToString());
                        oHistoriaClinicaDTO.P_Infartos_Cuando = dr["P_Infartos_Cuando"].ToString();
                        oHistoriaClinicaDTO.P_Varices = Convert.ToBoolean(dr["P_Varices"].ToString());
                        oHistoriaClinicaDTO.P_Varices_Lugar = dr["P_Varices_Lugar"].ToString();
                        oHistoriaClinicaDTO.P_Anticoagulantes = Convert.ToBoolean(dr["P_Anticoagulantes"].ToString());
                        oHistoriaClinicaDTO.P_Anticoagulantes_Dosis = dr["P_Anticoagulantes_Dosis"].ToString();
                        oHistoriaClinicaDTO.P_Aspirina = Convert.ToBoolean(dr["P_Aspirina"].ToString());
                        oHistoriaClinicaDTO.P_Aspirina_Dosis = dr["P_Aspirina_Dosis"].ToString();
                        oHistoriaClinicaDTO.P_Anticonceptivos = Convert.ToBoolean(dr["P_Anticonceptivos"].ToString());
                        oHistoriaClinicaDTO.P_Anticonceptivos_Dosis = dr["P_Anticonceptivos_Dosis"].ToString();
                        oHistoriaClinicaDTO.P_Colageno = Convert.ToBoolean(dr["P_Colageno"].ToString());
                        oHistoriaClinicaDTO.P_Colageno_Cual = dr["P_Colageno_Cual"].ToString();
                        oHistoriaClinicaDTO.P_Tiroides = Convert.ToBoolean(dr["P_Tiroides"].ToString());
                        oHistoriaClinicaDTO.P_Tiroides_Medicacion = dr["P_Tiroides_Medicacion"].ToString();
                        oHistoriaClinicaDTO.P_Cancer = Convert.ToBoolean(dr["P_Cancer"].ToString());
                        oHistoriaClinicaDTO.P_Cancer_Lugar = dr["P_Cancer_Lugar"].ToString();
                        oHistoriaClinicaDTO.P_Mentales = Convert.ToBoolean(dr["P_Mentales"].ToString());
                        oHistoriaClinicaDTO.P_Mentales_Medicacion = dr["P_Mentales_Medicacion"].ToString();
                        oHistoriaClinicaDTO.P_Convulsiones = Convert.ToBoolean(dr["P_Convulsiones"].ToString());
                        oHistoriaClinicaDTO.P_Convulsiones_Medicacion = dr["P_Convulsiones_Medicacion"].ToString();
                        oHistoriaClinicaDTO.FechaUltimaRegla = Convert.ToDateTime(dr["FechaUltimaRegla"].ToString());
                        oHistoriaClinicaDTO.Embarazos = dr["Embarazos"].ToString();
                        oHistoriaClinicaDTO.AntecedentesQx = dr["AntecedentesQx"].ToString();
                        oHistoriaClinicaDTO.Alergias = dr["Alergias"].ToString();
                        oHistoriaClinicaDTO.MedicacionPaciente = dr["MedicacionPaciente"].ToString();
                        oHistoriaClinicaDTO.EF_ImpresionGeneral = dr["EF_ImpresionGeneral"].ToString();
                        oHistoriaClinicaDTO.EF_SignosVitales_FC = dr["EF_SignosVitales_FC"].ToString();
                        oHistoriaClinicaDTO.EF_SignosVitales_TA = dr["EF_SignosVitales_TA"].ToString();
                        oHistoriaClinicaDTO.EF_SignosVitales_FR = dr["EF_SignosVitales_FR"].ToString();
                        oHistoriaClinicaDTO.EF_SignosVitales_Pulso = dr["EF_SignosVitales_Pulso"].ToString();
                        oHistoriaClinicaDTO.EF_TAuxiliar = dr["EF_TAuxiliar"].ToString();
                        oHistoriaClinicaDTO.EF_PesoHabitual = Convert.ToDecimal(dr["EF_PesoHabitual"].ToString());
                        oHistoriaClinicaDTO.EF_PesoActual = Convert.ToDecimal(dr["EF_PesoActual"].ToString());
                        oHistoriaClinicaDTO.EF_Talla = Convert.ToDecimal(dr["EF_Talla"].ToString());
                        oHistoriaClinicaDTO.EF_IMC = dr["EF_IMC"].ToString();
                        oHistoriaClinicaDTO.Nutricion = Convert.ToBoolean(dr["Nutricion"].ToString());
                        oHistoriaClinicaDTO.N_Calorias_Dia = dr["N_Calorias_Dia"].ToString();
                        oHistoriaClinicaDTO.N_NroVecesAlimentacion = dr["N_NroVecesAlimentacion"].ToString();
                        oHistoriaClinicaDTO.N_PorcentajeGrasa = dr["N_PorcentajeGrasa"].ToString();
                        oHistoriaClinicaDTO.N_DietaRegular = dr["N_DietaRegular"].ToString();
                        oHistoriaClinicaDTO.N_PreferenciasAlimentarias = dr["N_PreferenciasAlimentarias"].ToString();
                        oHistoriaClinicaDTO.N_Objetivo = dr["N_Objetivo"].ToString();
                        oHistoriaClinicaDTO.ExamenComplementario = dr["ExamenComplementario"].ToString();
                        oHistoriaClinicaDTO.OtrosEstudios = dr["OtrosEstudios"].ToString();
                        oHistoriaClinicaDTO.FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString());
                        oHistoriaClinicaDTO.FechaModificacion = Convert.ToDateTime(dr["FechaModificacion"].ToString());
                        oHistoriaClinicaDTO.UsuarioCreacion = Convert.ToInt32(dr["UsuarioCreacion"].ToString());
                        oHistoriaClinicaDTO.UsuarioModificacion = Convert.ToInt32(dr["UsuarioModificacion"].ToString());
                        oHistoriaClinicaDTO.Estado = Convert.ToBoolean(dr["Estado"].ToString());
                        oResultDTO.ListaResultado.Add(oHistoriaClinicaDTO);
                    }
                    if (oResultDTO.ListaResultado.Count > 0)
                    {
                        if (dr.NextResult())
                        {
                            List<HistoriaClinica_ArchivosDTO> listaArchivo = new List<HistoriaClinica_ArchivosDTO>();
                            while (dr.Read())
                            {
                                HistoriaClinica_ArchivosDTO objArchivo = new HistoriaClinica_ArchivosDTO();
                                objArchivo.idHistoriaArchivo = Convert.ToInt32(dr["idHistoriaArchivo"].ToString());
                                objArchivo.idHistoria = Convert.ToInt32(dr["idHistoria"].ToString());
                                objArchivo.NombreArchivo = dr["NombreArchivo"].ToString();
                                objArchivo.Fecha = Convert.ToDateTime(dr["Fecha"].ToString());
                                objArchivo.Extension = dr["Extension"].ToString();
                                objArchivo.Archivo = dr["Archivo"].ToString();
                                listaArchivo.Add(objArchivo);
                            }
                            oResultDTO.ListaResultado[0].oListaArchivos = listaArchivo;
                        }
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<HistoriaClinicaDTO>();
                }
            }
            return oResultDTO;
        }
        public ResultDTO<HistoriaClinicaDTO> UpdateInsert(HistoriaClinicaDTO oHistoriaClinica)
        {
            ResultDTO<HistoriaClinicaDTO> oResultDTO = new ResultDTO<HistoriaClinicaDTO>();
            var option = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted,
                Timeout = TimeSpan.FromSeconds(60)
            };
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, option))
            {
                using (SqlConnection cn = new Conexion().conectar())
                {
                    try
                    {
                        cn.Open();
                        SqlDataAdapter da = new SqlDataAdapter("SP_HistoriaClinica_UpdateInsert", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idHistoria", oHistoriaClinica.idHistoria);
                        da.SelectCommand.Parameters.AddWithValue("@Codigo", oHistoriaClinica.Codigo);
                        da.SelectCommand.Parameters.AddWithValue("@idPaciente", oHistoriaClinica.idPaciente);
                        da.SelectCommand.Parameters.AddWithValue("@NombrePaciente", oHistoriaClinica.NombrePaciente);
                        da.SelectCommand.Parameters.AddWithValue("@Edad", oHistoriaClinica.Edad);
                        da.SelectCommand.Parameters.AddWithValue("@Sexo", oHistoriaClinica.Sexo);
                        da.SelectCommand.Parameters.AddWithValue("@FechaNacimiento", oHistoriaClinica.FechaNacimiento.ToShortDateString());
                        da.SelectCommand.Parameters.AddWithValue("@Dni", oHistoriaClinica.Dni);
                        da.SelectCommand.Parameters.AddWithValue("@FechaActual", oHistoriaClinica.FechaActual.ToShortDateString());
                        //da.SelectCommand.Parameters.AddWithValue("@MotivoConsulta", oHistoriaClinica.MotivoConsulta);
                        da.SelectCommand.Parameters.AddWithValue("@AntecedentesFamiliares", oHistoriaClinica.AntecedentesFamiliares);
                        da.SelectCommand.Parameters.AddWithValue("@AF_Padres", oHistoriaClinica.AF_Padres);
                        da.SelectCommand.Parameters.AddWithValue("@AF_Padres_Vivos", oHistoriaClinica.AF_Padres_Vivos);
                        da.SelectCommand.Parameters.AddWithValue("@AF_Padres_Fallecidos", oHistoriaClinica.AF_Padres_Fallecidos);
                        da.SelectCommand.Parameters.AddWithValue("@AF_Padres_Causas", oHistoriaClinica.AF_Padres_Causas);
                        da.SelectCommand.Parameters.AddWithValue("@AF_Hermanos", oHistoriaClinica.AF_Hermanos);
                        da.SelectCommand.Parameters.AddWithValue("@AF_Hermanos_Vivos", oHistoriaClinica.AF_Hermanos_Vivos);
                        da.SelectCommand.Parameters.AddWithValue("@AF_Hermanos_Fallecidos", oHistoriaClinica.AF_Hermanos_Fallecidos);
                        da.SelectCommand.Parameters.AddWithValue("@AF_Hermanos_Causas", oHistoriaClinica.AF_Hermanos_Causas);
                        da.SelectCommand.Parameters.AddWithValue("@AF_Hijos", oHistoriaClinica.AF_Hijos);
                        da.SelectCommand.Parameters.AddWithValue("@AF_Hijos_Vivos", oHistoriaClinica.AF_Hijos_Vivos);
                        da.SelectCommand.Parameters.AddWithValue("@AF_Hijos_Fallecidos", oHistoriaClinica.AF_Hijos_Fallecidos);
                        da.SelectCommand.Parameters.AddWithValue("@AF_Hijos_Causas", oHistoriaClinica.AF_Hijos_Causas);
                        da.SelectCommand.Parameters.AddWithValue("@AP_Alcohol", oHistoriaClinica.AP_Alcohol);
                        da.SelectCommand.Parameters.AddWithValue("@AP_tabaco", oHistoriaClinica.AP_tabaco);
                        da.SelectCommand.Parameters.AddWithValue("@AP_Drogas", oHistoriaClinica.AP_Drogas);
                        da.SelectCommand.Parameters.AddWithValue("@AP_Transfusiones", oHistoriaClinica.AP_Transfusiones);
                        da.SelectCommand.Parameters.AddWithValue("@P_Diabetis", oHistoriaClinica.P_Diabetis);
                        da.SelectCommand.Parameters.AddWithValue("@P_Hipertension", oHistoriaClinica.P_Hipertension);
                        da.SelectCommand.Parameters.AddWithValue("@P_Tubercolosis", oHistoriaClinica.P_Tuberculosis);
                        da.SelectCommand.Parameters.AddWithValue("@P_Hepatitis", oHistoriaClinica.P_Hepatitis);
                        da.SelectCommand.Parameters.AddWithValue("@P_Vih", oHistoriaClinica.P_Vih);
                        da.SelectCommand.Parameters.AddWithValue("@P_Sifilis", oHistoriaClinica.P_Sifilis);
                        da.SelectCommand.Parameters.AddWithValue("@P_Asma", oHistoriaClinica.P_Asma);
                        da.SelectCommand.Parameters.AddWithValue("@P_Gota", oHistoriaClinica.P_Gota);
                        da.SelectCommand.Parameters.AddWithValue("@P_Hipercolesterolemia", oHistoriaClinica.P_Hipercolesterolemia);
                        da.SelectCommand.Parameters.AddWithValue("@P_Migraña", oHistoriaClinica.P_Migraña);
                        da.SelectCommand.Parameters.AddWithValue("@P_EnfVascular", oHistoriaClinica.P_EnfVascular);
                        da.SelectCommand.Parameters.AddWithValue("@P_EnfVascular_Cuando", oHistoriaClinica.P_EnfVascular_Cuando);
                        da.SelectCommand.Parameters.AddWithValue("@P_Infartos", oHistoriaClinica.P_Infartos);
                        da.SelectCommand.Parameters.AddWithValue("@P_Infartos_Cuando", oHistoriaClinica.P_Infartos_Cuando);
                        da.SelectCommand.Parameters.AddWithValue("@P_Varices", oHistoriaClinica.P_Varices);
                        da.SelectCommand.Parameters.AddWithValue("@P_Varices_Lugar", oHistoriaClinica.P_Varices_Lugar);
                        da.SelectCommand.Parameters.AddWithValue("@P_Anticoagulantes", oHistoriaClinica.P_Anticoagulantes);
                        da.SelectCommand.Parameters.AddWithValue("@P_Anticoagulantes_Dosis", oHistoriaClinica.P_Anticoagulantes_Dosis);
                        da.SelectCommand.Parameters.AddWithValue("@P_Aspirina", oHistoriaClinica.P_Aspirina);
                        da.SelectCommand.Parameters.AddWithValue("@P_Aspirina_Dosis", oHistoriaClinica.P_Aspirina_Dosis);
                        da.SelectCommand.Parameters.AddWithValue("@P_Anticonceptivos", oHistoriaClinica.P_Anticonceptivos);
                        da.SelectCommand.Parameters.AddWithValue("@P_Anticonceptivos_Dosis", oHistoriaClinica.P_Anticonceptivos_Dosis);
                        da.SelectCommand.Parameters.AddWithValue("@P_Colageno", oHistoriaClinica.P_Colageno);
                        da.SelectCommand.Parameters.AddWithValue("@P_Colageno_Cual", oHistoriaClinica.P_Colageno_Cual);
                        da.SelectCommand.Parameters.AddWithValue("@P_Tiroides", oHistoriaClinica.P_Tiroides);
                        da.SelectCommand.Parameters.AddWithValue("@P_Tiroides_Medicacion", oHistoriaClinica.P_Tiroides_Medicacion);
                        da.SelectCommand.Parameters.AddWithValue("@P_Cancer", oHistoriaClinica.P_Cancer);
                        da.SelectCommand.Parameters.AddWithValue("@P_Cancer_Lugar", oHistoriaClinica.P_Cancer_Lugar);
                        da.SelectCommand.Parameters.AddWithValue("@P_Mentales", oHistoriaClinica.P_Mentales);
                        da.SelectCommand.Parameters.AddWithValue("@P_Mentales_Medicacion", oHistoriaClinica.P_Mentales_Medicacion);
                        da.SelectCommand.Parameters.AddWithValue("@P_Convulsiones", oHistoriaClinica.P_Convulsiones);
                        da.SelectCommand.Parameters.AddWithValue("@P_Convulsiones_Medicacion", oHistoriaClinica.P_Convulsiones_Medicacion);
                        da.SelectCommand.Parameters.AddWithValue("@FechaUltimaRegla", oHistoriaClinica.FechaUltimaRegla.ToShortDateString());
                        da.SelectCommand.Parameters.AddWithValue("@Embarazos", oHistoriaClinica.Embarazos);
                        da.SelectCommand.Parameters.AddWithValue("@AntecedentesQx", oHistoriaClinica.AntecedentesQx);
                        da.SelectCommand.Parameters.AddWithValue("@Alergias", oHistoriaClinica.Alergias);
                        da.SelectCommand.Parameters.AddWithValue("@MedicacionPaciente", oHistoriaClinica.MedicacionPaciente);
                        da.SelectCommand.Parameters.AddWithValue("@Otros", oHistoriaClinica.Otros);
                        da.SelectCommand.Parameters.AddWithValue("@EF_ImpresionGeneral", oHistoriaClinica.EF_ImpresionGeneral);
                        da.SelectCommand.Parameters.AddWithValue("@EF_SignosVitales_FC", oHistoriaClinica.EF_SignosVitales_FC);
                        da.SelectCommand.Parameters.AddWithValue("@EF_SignosVitales_TA", oHistoriaClinica.EF_SignosVitales_TA);
                        da.SelectCommand.Parameters.AddWithValue("@EF_SignosVitales_FR", oHistoriaClinica.EF_SignosVitales_FR);
                        da.SelectCommand.Parameters.AddWithValue("@EF_SignosVitales_Pulso", oHistoriaClinica.EF_SignosVitales_Pulso);
                        da.SelectCommand.Parameters.AddWithValue("@EF_TAuxiliar", oHistoriaClinica.EF_TAuxiliar);
                        da.SelectCommand.Parameters.AddWithValue("@EF_PesoHabitual", oHistoriaClinica.EF_PesoHabitual);
                        da.SelectCommand.Parameters.AddWithValue("@EF_PesoActual", oHistoriaClinica.EF_PesoActual);
                        da.SelectCommand.Parameters.AddWithValue("@EF_Talla", oHistoriaClinica.EF_Talla);
                        da.SelectCommand.Parameters.AddWithValue("@EF_IMC", oHistoriaClinica.EF_IMC);
                        da.SelectCommand.Parameters.AddWithValue("@Nutricion", oHistoriaClinica.Nutricion);
                        da.SelectCommand.Parameters.AddWithValue("@N_Calorias_Dia", oHistoriaClinica.N_Calorias_Dia);
                        da.SelectCommand.Parameters.AddWithValue("@N_NroVecesAlimentacion", oHistoriaClinica.N_NroVecesAlimentacion);
                        da.SelectCommand.Parameters.AddWithValue("@N_PorcentajeGrasa", oHistoriaClinica.N_PorcentajeGrasa);
                        da.SelectCommand.Parameters.AddWithValue("@N_DietaRegular", oHistoriaClinica.N_DietaRegular);
                        da.SelectCommand.Parameters.AddWithValue("@N_PreferenciasAlimentarias", oHistoriaClinica.N_PreferenciasAlimentarias);
                        da.SelectCommand.Parameters.AddWithValue("@N_Objetivo", oHistoriaClinica.N_Objetivo);
                        da.SelectCommand.Parameters.AddWithValue("@ExamenComplementario", oHistoriaClinica.ExamenComplementario);
                        da.SelectCommand.Parameters.AddWithValue("@OtrosEstudios", oHistoriaClinica.OtrosEstudios);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioCreacion", oHistoriaClinica.UsuarioCreacion);
                        da.SelectCommand.Parameters.AddWithValue("@UsuarioModificacion", oHistoriaClinica.UsuarioModificacion);
                        da.SelectCommand.Parameters.AddWithValue("@Estado", oHistoriaClinica.Estado);
                        da.SelectCommand.Parameters.AddWithValue("@lista_Archivos", oHistoriaClinica.lista_Archivos);

                        SqlParameter id_output = da.SelectCommand.Parameters.Add("@id", SqlDbType.Int);
                        id_output.Direction = ParameterDirection.Output;
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta > 0)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarTodo(cn).ListaResultado;
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<HistoriaClinicaDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<HistoriaClinicaDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public ResultDTO<HistoriaClinicaDTO> Delete(HistoriaClinicaDTO oHistoriaClinica)
        {
            ResultDTO<HistoriaClinicaDTO> oResultDTO = new ResultDTO<HistoriaClinicaDTO>();
            var option = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted,
                Timeout = TimeSpan.FromSeconds(60)
            };
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, option))
            {
                using (SqlConnection cn = new Conexion().conectar())
                {
                    try
                    {
                        cn.Open();
                        SqlDataAdapter da = new SqlDataAdapter("SP_HistoriaClinica_Delete", cn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@idHistoria", oHistoriaClinica.idHistoria);
                        int rpta = da.SelectCommand.ExecuteNonQuery();
                        if (rpta == 1)
                        {
                            oResultDTO.Resultado = "OK";
                            oResultDTO.ListaResultado = ListarTodo(cn).ListaResultado;
                            transactionScope.Complete();
                        }
                        else
                        {
                            oResultDTO.Resultado = "Error";
                            oResultDTO.ListaResultado = new List<HistoriaClinicaDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        oResultDTO.Resultado = "Error";
                        oResultDTO.MensajeError = ex.Message;
                        oResultDTO.ListaResultado = new List<HistoriaClinicaDTO>();
                    }
                }
            }
            return oResultDTO;
        }
        public string NroHistoriaUltimo()
        {
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_LM_HistoriaClinica_UltimoNroHistoria", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    var nroHistoria = da.SelectCommand.ExecuteScalar();
                    return nroHistoria.ToString();
                }
                catch (Exception ex)
                {
                    return "0";
                }
            }
        }
        public ResultDTO<HistoriaClinica_ArchivosDTO> GetFileArchivo(int idHistoriaArchivo)
        {
            ResultDTO<HistoriaClinica_ArchivosDTO> oResultDTO = new ResultDTO<HistoriaClinica_ArchivosDTO>();
            oResultDTO.ListaResultado = new List<HistoriaClinica_ArchivosDTO>();
            using (SqlConnection cn = new Conexion().conectar())
            {
                try
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SP_LM_HistoriaClinica_Archivos_GetFile", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idHistoriaArchivo", idHistoriaArchivo);
                    SqlDataReader dr = da.SelectCommand.ExecuteReader();
                    while (dr.Read())
                    {
                        HistoriaClinica_ArchivosDTO oHistoriaClinica_ArchivosDTO = new HistoriaClinica_ArchivosDTO();
                        oHistoriaClinica_ArchivosDTO.Archivo = dr["Archivo"].ToString();
                        oHistoriaClinica_ArchivosDTO.NombreArchivo = dr["NombreArchivo"].ToString();
                        oResultDTO.ListaResultado.Add(oHistoriaClinica_ArchivosDTO);
                    }
                    oResultDTO.Resultado = "OK";
                }
                catch (Exception ex)
                {
                    oResultDTO.Resultado = "Error";
                    oResultDTO.MensajeError = ex.Message;
                    oResultDTO.ListaResultado = new List<HistoriaClinica_ArchivosDTO>();
                }
            }
            return oResultDTO;
        }
    }
}
