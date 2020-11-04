using Microsoft.EntityFrameworkCore;
using Parcial2_Ap2_Wilbert.DAL;
using Parcial2_Ap2_Wilbert.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Parcial2_Ap2_Wilbert.BLL
{
    public class ClientesBLL
    {
        public static bool Guardar(Clientes clientes)
        {
            if (!Existe(clientes.IdCliente))
                return Insertar(clientes);
            else
                return Modificar(clientes);
        }

        private static bool Insertar(Clientes clientes)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                
                contexto.Clientes.Add(clientes);
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }

        public static bool Modificar(Clientes clientes)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Database.ExecuteSqlRaw($"Delete FROM EstudiantesDetalle Where EstudianteId = {estudiante.EstudianteId}");

                foreach (var item in clientes.)
                {
                    contexto.Entry(item).State = EntityState.Added;
                }

                //marcar la entidad como modificada para que el contexto sepa como proceder
                contexto.Entry(clientes).State = EntityState.Modified;
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }

        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                var estudiante = contexto.Clientes.Find(id);

                if (estudiante != null)
                {
                    contexto.Clientes.Remove(estudiante);
                    paso = contexto.SaveChanges() > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }

        public static Clientes Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Clientes estudiante;

            try
            {
                estudiante = contexto.Clientes
                    .Where(e => e.IdCliente == id)
                    .Include(e => e.EstudiantesDetalle)
                    .FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return estudiante;
        }

        public static List<Clientes> GetList(Expression<Func<Clientes, bool>> criterio)
        {
            List<Clientes> lista = new List<Clientes>();
            Contexto contexto = new Contexto();
            try
            {
               
                lista = contexto.Clientes.Where(criterio).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return lista;
        }

        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool encontrado = false;

            try
            {
                encontrado = contexto.Clientes.Any(e => e.IdCliente == id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return encontrado;
        }


    }
}
