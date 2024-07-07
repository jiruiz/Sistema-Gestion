using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Negocio
{
    public class CNDeposito
    {
        public List<Deposito> ObtenerDepositos()
        {   
            //USANDO EL CONTEXTO
            using (var context = new MyDbContext()){
                var depositos = context.Deposito.ToList();
                
                // SI LA LISTA DEPOSITOS ES NULA O ES IGUAL A 0 AVISA QUE NO LA ENCUENTRA
                if (depositos == null || depositos.Count == 0)
                {

                    Console.WriteLine("No se encontraron depositos en la lista (LOGICA DE CAPA NEGOCIO).");
                }
                return depositos;
            }    
        }


        //PARA NO DECLARAR PK Y FK EN LA BASE DE DATOS, YA QUE PUEDE TRAER CONFLICTOS, ENTONCES OPERO
        //CON EL ULTIMO VALOR ID DE LA BASE DE DATOS Y LE SUMO 1, ESE ES EL VALOR QUE SE VA A GUARDAR EN 
        //EL SIGUIENTE REGISTRO
        public int ObtenerNuevoCodigoID()
        {
            using (var context = new MyDbContext())
            {
                //ACA BUSCO EL ULTIMO VALOR, LOS ORDENO POR DESCENDENCI (DEL MAS GRANDE AL MAS CHICO), Y LUEGO 
                //OBTENGO EL PRIMERO
                var ultimoDeposito = context.Deposito.OrderByDescending(d => d.Id).FirstOrDefault();

                //SI NO HAY NADA, LO ESTABLECE EN 1
                if (ultimoDeposito == null)
                    return 1;

                //SI ENCUENTRA ALGO LE SUMA 1
                return ultimoDeposito.Id + 1;
            }
        }




        //AGREGAMOS LOS VALORES OBTENIDOS DESDE EL FORM DE DEPOSITO
        //Y LO AÑADIMOS A LA BASE DE DATOS DEL CONTEXTO QUE SE REQUIERA (DEPOSITO EN ESTE CASO)
        public void AgregarDeposito(Deposito deposito)
        {
            //USANDO EL CONTEXTO BASE
            using (var context = new MyDbContext())
            {
                //AÑADIMOS LOS VALORES Y GUARDAMOS LOS CAMBIOS
                context.Deposito.Add(deposito);
                context.SaveChanges();
            }
        }


        //FUNCION QUE MODIFICA EL DATO DE LA GRILLA DE DEPOSITOS
        //MIENTRAS USAMOS EN EL CONTEXTO DE DEPOSITO, BUSCANDO EL ID
        public void ModificarDeposito(Deposito deposito)
        {
            try
            {   //USAMOS EL CONTEXTO BASE
                using (var context = new MyDbContext())
                {
                    //OBTENEMOS EL ID COMO SE MENCIONA MAS ARRIBA
                    var depositoExistente = context.Deposito.Find(deposito.Id);

                    //SI HAY UN DEPOSITO EXISTENTE CON ESE ID OBTENIDO
                    if (depositoExistente != null)
                    {
                        //SI LO HAY, ACTUALIZAMOS LOS VALORES DE LA FILA, EN CADA UNA DE SUS CELDAS
                        depositoExistente.FechaDeposito = deposito.FechaDeposito;
                        depositoExistente.ImporteDeposito = deposito.ImporteDeposito;
                        depositoExistente.BancoDeposito = deposito.BancoDeposito;
                        depositoExistente.ObservacionesDepositos = deposito.ObservacionesDepositos;
                        depositoExistente.idAgencia = deposito.idAgencia;   

                        
                        //LE AVISAMOS A ENTITY FRAMEWORS QUE SE REALIZARON CAMBIOS
                        context.Entry(depositoExistente).State = EntityState.Modified;

                        //Y FINALMENTE GUARDAMOS LOS CAMBIOS
                        context.SaveChanges();
                    }
                    else
                    {
                        
                        throw new Exception($"No se encontró el depósito con ID {deposito.Id}");
                    }
                }
            }
            catch (Exception ex)
            {
                ObtenerDepositos();
                throw new Exception("Error al modificar depósito (lógica de capa negocio).", ex);
            }
        }



        public void EliminarDeposito(int depositoID)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    var depositoAEliminar = context.Deposito.Find(depositoID);

                    if (depositoAEliminar != null)
                    {
                        context.Deposito.Remove(depositoAEliminar);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception($"No se encontró el deposito con ID {depositoID}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la el deposito (LOGICA DE CAPA NEGOCIO).", ex);
            }
        }

        public void EliminarTodosLosDepositos()
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    // Obtener todos los depósitos
                    var depositos = context.Deposito.ToList();

                    // Eliminar cada depósito de la base de datos
                    foreach (var deposito in depositos)
                    {
                        context.Deposito.Remove(deposito);
                    }

                    // Guardar los cambios en la base de datos
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar todos los depósitos (LOGICA DE CAPA NEGOCIO).", ex);
            }
        }

    }

}
