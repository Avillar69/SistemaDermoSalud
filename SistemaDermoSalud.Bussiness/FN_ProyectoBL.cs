using SistemaDermoSalud.DataAccess;
using SistemaDermoSalud.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDermoSalud.Business
{
    public class FN_ProyectoBL
    {
        FN_ProyectoDAO OFN_Proyecto = new FN_ProyectoDAO();
        public ResultDTO<FN_ProyectosDTO> ListarTodo(int idEmpresa)
        {
            return OFN_Proyecto.ListarTodo(idEmpresa);
        }
        public ResultDTO<FN_ProyectosDTO> ListarxID(int idProyecto)
        {
            return OFN_Proyecto.ListarxID(idProyecto);
        }
        public ResultDTO<FN_ProyectosDTO> UpdateInsert(FN_ProyectosDTO oFN_ProyectoDTO)
        {
            ResultDTO<FN_ProyectosDTO> oResultDTO = OFN_Proyecto.UpdateInsert(oFN_ProyectoDTO);
            return oResultDTO;
        }
        public ResultDTO<FN_ProyectosDTO> Delete(FN_ProyectosDTO oFN_ProyectoDTO)
        {
            ResultDTO<FN_ProyectosDTO> oResultDTO = OFN_Proyecto.Delete(oFN_ProyectoDTO);
            return oResultDTO;
        }
        public ResultDTO<FN_ProyectosDTO> ListarProyectosCaja()
        {
            return OFN_Proyecto.ListarProyectosCaja();
        }
    }
}
