using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class CNAgencia
    {
        public List<Agencia> ObtenerAgencias()
        {
            using (var context = new MyDbContext())
            {
                // Verificar si realmente está obteniendo datos
                var agencias = context.Agencia.ToList();

                if (agencias == null || agencias.Count == 0)
                {
                    // Opcional: puedes hacer un log aquí para verificar si realmente no hay datos
                    Console.WriteLine("No se encontraron agencias (LOGICA DE CAPA NEGOCIO).");
                }

                return agencias;
            }
        }

        public void AgregarAgencia(Agencia agencia)
        {
            try
            {
                using (var context = new MyDbContext()) 
                {
                    context.Agencia.Add(agencia);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Error al agregar el Agencia  (LOGICA DE CAPA NEGOCIO).", ex);

            }

        }

        public void EliminarAgencia(int agenciaId)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    var agenciaAEliminar = context.Agencia.Find(agenciaId);

                    if (agenciaAEliminar != null)
                    {
                        context.Agencia.Remove(agenciaAEliminar);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception($"No se encontró la Agencia con ID {agenciaId}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la Agencia (LOGICA DE CAPA NEGOCIO).", ex);
            }
        }


        

        public void ModificarAgencia(Agencia agencia)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    var existingAgencia = context.Agencia.Find(agencia.Id);

                    if (existingAgencia != null)
                    {
                        // Actualizar las propiedades de la agencia existente
                        existingAgencia.Nombre = agencia.Nombre;

                        // Marcar la entidad como modificada para que Entity Framework realice la actualización en la base de datos
                        context.Entry(existingAgencia).State = System.Data.Entity.EntityState.Modified;

                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception($"No se encontró la Agencia con ID {agencia.Id}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar la Agencia (LOGICA DE CAPA NEGOCIO).", ex);
            }
        }

    }
}
