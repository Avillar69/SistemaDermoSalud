using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SistemaDermoSalud.Helpers
{
    public class Serializador
    {

        public static string Serializar<T>(List<T> lista, char separadorCampo, char separadorRegistro, string[] campos, bool incluirCabeceras = true)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sbc = new StringBuilder();
            if (lista != null && lista.Count > 0)
            {
                PropertyInfo[] propiedades = lista[0].GetType().GetProperties();
                if (campos.Length == 0)
                {
                    if (incluirCabeceras)
                    {
                        for (int i = 0; i < propiedades.Length - 1; i++)
                        {
                            sbc.Append(propiedades[i].Name);
                            if (i < propiedades.Length - 1) sbc.Append(separadorCampo);
                        }
                        sb.Append(separadorRegistro);
                    }
                    string cabece;
                    string tipo;
                    object valor;
                    for (int j = 0; j < lista.Count; j++)
                    {
                        propiedades = lista[j].GetType().GetProperties();
                        for (int i = 0; i < propiedades.Length; i++)
                        {
                            cabece = propiedades[i].Name;
                            tipo = propiedades[i].PropertyType.ToString();
                            valor = propiedades[i].GetValue(lista[j], null);
                            if (valor != null)
                            {
                                if (tipo.Contains("Byte[]")) //para imagenes
                                {
                                    byte[] buffer = (byte[])valor;
                                    sb.Append(Convert.ToBase64String(buffer));
                                }
                                else if (tipo.Contains("DateTime")) // para fechas
                                {
                                    var dvalor = Convert.ToDateTime(valor);
                                    sb.Append(dvalor.ToString("dd-MM-yyyy"));
                                }
                                else if (tipo.Contains("String") && cabece.ToLower() == "estado")
                                {

                                }
                                else if (tipo.ToLower().Contains("bool") && cabece.ToLower() == "estado")
                                {
                                    if (valor.ToString().ToUpper() == "TRUE") { sb.Append("ACTIVO"); } else { sb.Append("INACTIVO"); }
                                }
                                else if (cabece == "Imagen" || cabece == "Archivo")
                                {
                                    sb.Append(valor.ToString());
                                }

                                else sb.Append(valor.ToString().ToUpper());
                            }
                            else sb.Append("");
                            if (i < propiedades.Length - 1) sb.Append(separadorCampo);
                        }
                        if (j < lista.Count - 1) sb.Append(separadorRegistro);
                    }
                }
                else
                {
                    //if (File.Exists(archivo))
                    //{
                    //List<string> campos = File.ReadAllLines(archivo).ToList();
                    List<string> props = new List<string>();
                    for (int i = 0; i < propiedades.Length; i++)
                    {
                        props.Add(propiedades[i].Name);
                    }
                    if (incluirCabeceras)
                    {
                        for (int i = 0; i < campos.Length; i++)
                        {
                            if (props.IndexOf(campos[i]) > -1)
                            {
                                sb.Append(campos[i]);
                                sb.Append(separadorCampo);
                            }
                        }
                        sb = sb.Remove(sb.Length - 1, 1);
                        sb.Append(separadorRegistro);
                    }
                    string cabece;
                    string tipo;
                    object valor;
                    for (int j = 0; j < lista.Count; j++)
                    {
                        for (int i = 0; i < campos.Length; i++)
                        {
                            if (props.IndexOf(campos[i]) > -1)
                            {
                                cabece = lista[j].GetType().GetProperty(campos[i]).Name;
                                tipo = lista[j].GetType().GetProperty(campos[i]).PropertyType.ToString();
                                valor = lista[j].GetType().GetProperty(campos[i]).GetValue(lista[j], null);
                                if (valor != null)
                                {
                                    if (tipo.Contains("Byte[]")) //para imagenes
                                    {
                                        byte[] buffer = (byte[])valor;
                                        sb.Append(Convert.ToBase64String(buffer));
                                    }
                                    else if (tipo.Contains("DateTime")) // para fechas
                                    {
                                        var dvalor = Convert.ToDateTime(valor);
                                        sb.Append(dvalor.ToString("dd/MM/yyyy"));
                                    }
                                    else if (tipo.Contains("String") && cabece.ToLower() == "estado")
                                    {
                                        if (valor.ToString().ToUpper() == "A") { sb.Append("ACTIVO"); } else { sb.Append("INACTIVO"); }
                                    }
                                    else if (tipo.ToLower().Contains("bool") && cabece.ToLower() == "estado")
                                    {
                                        if (valor.ToString().ToUpper() == "TRUE") { sb.Append("ACTIVO"); } else { sb.Append("INACTIVO"); }
                                    }
                                    else if (cabece == "Imagen")
                                    {
                                        sb.Append(valor.ToString());
                                    }
                                    else sb.Append(valor.ToString().ToUpper());
                                }
                                else sb.Append("");
                                sb.Append(separadorCampo);
                            }
                        }
                        sb = sb.Remove(sb.Length - 1, 1);
                        sb.Append(separadorRegistro);
                    }
                    sb = sb.Remove(sb.Length - 1, 1);
                    //}
                }
            }
            return sb.ToString();
        }
        public static List<T> Deserializar<T>(string archivo, char separadorCampo, char separadorRegistro)
        {
            List<T> lista = new List<T>();
            if (File.Exists(archivo))
            {
                string contenido = File.ReadAllText(archivo);
                string[] registros = contenido.Split(separadorRegistro);
                string[] campos;
                string[] cabecera = registros[0].Split(separadorCampo);
                string registro;
                Type tipoObj;
                T obj;
                dynamic valor;
                Type tipoCampo;
                for (int i = 1; i < registros.Length; i++)
                {
                    registro = registros[i];
                    tipoObj = typeof(T);
                    obj = (T)Activator.CreateInstance(tipoObj);
                    campos = registro.Split(separadorCampo);
                    for (int j = 0; j < campos.Length; j++)
                    {
                        tipoCampo = obj.GetType().GetProperty(cabecera[j]).PropertyType;
                        valor = Convert.ChangeType(campos[j], tipoCampo);
                        obj.GetType().GetProperty(cabecera[j]).SetValue(obj, valor);
                    }
                    lista.Add(obj);
                }
            }
            return (lista);
        }
        public static string SerializarObjeto<T>(T obj, char separadorCampo, string archivo = "")
        {
            StringBuilder sb = new StringBuilder();
            PropertyInfo[] propiedades = obj.GetType().GetProperties();
            string tipo;
            object valor;
            if (archivo == "")
            {
                for (int i = 0; i < propiedades.Length; i++)
                {
                    tipo = propiedades[i].PropertyType.ToString();
                    if (propiedades[i].GetValue(obj, null) != null)
                    {
                        if (tipo.Contains("Byte[]"))
                        {
                            byte[] buffer = (byte[])propiedades[i].GetValue(obj, null);
                            sb.Append(Convert.ToBase64String(buffer));
                        }
                        else sb.Append(propiedades[i].GetValue(obj, null).ToString());
                    }
                    else sb.Append("");
                    if (i < propiedades.Length - 1) sb.Append(separadorCampo);
                }
            }
            else
            {
                if (File.Exists(archivo))
                {
                    List<string> campos = File.ReadAllLines(archivo).ToList();
                    List<string> props = new List<string>();
                    for (int i = 0; i < propiedades.Length; i++)
                    {
                        props.Add(propiedades[i].Name);
                    }
                    for (int i = 0; i < campos.Count; i++)
                    {
                        if (props.IndexOf(campos[i]) > -1)
                        {
                            tipo = obj.GetType().GetProperty(campos[i]).PropertyType.ToString();
                            valor = obj.GetType().GetProperty(campos[i]).GetValue(obj, null);
                            if (valor != null)
                            {
                                if (tipo.Contains("Byte[]"))
                                {
                                    byte[] buffer = (byte[])valor;
                                    sb.Append(Convert.ToBase64String(buffer));
                                }
                                else sb.Append(valor.ToString());
                            }
                            else sb.Append("");
                            sb.Append(separadorCampo);
                        }
                    }
                    sb = sb.Remove(sb.Length - 1, 1);
                }
            }
            return sb.ToString();
        }
        public static string rSerializado<T>(List<T> lista, string[] campos)
        {
            if (lista != null && lista.Count > 0)
            {
                return Serializar(lista, '▲', '▼', campos, false);
            }
            else
            {
                return "";
            }

        }
    }
}
