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
    public class CobrosBLL
    {
        public static bool Guardar(Cobros cobros)
        {
            if (!Existe(cobros.IdCobros))
                return Insertar(cobros);
            else
                return Modificar(cobros);
        }

        private static bool Insertar(Cobros cobros)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {

                contexto.Cobros.Add(cobros);
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

        public static bool Modificar(Cobros cobros)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Database.ExecuteSqlRaw($"Delete FROM CobrosDetalle Where CobroId = {cobros.IdCobros}");

                foreach (var item in cobros.cobrosDetalle)
                {
                    contexto.Entry(item).State = EntityState.Added;
                }
                contexto.Entry(cobros).State = EntityState.Modified;
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
                var cobros = contexto.Cobros.Find(id);

                if (cobros != null)
                {
                    contexto.Cobros.Remove(cobros);
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

        public static Cobros Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Cobros cobros;

            try
            {
                cobros = contexto.Cobros
                    .Where(e => e.IdCobros == id)
                    .Include(e => e.cobrosDetalle)
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

            return cobros;
        }

        public static List<Cobros> GetList(Expression<Func<Cobros, bool>> criterio)
        {
            List<Cobros> lista = new List<Cobros>();
            Contexto contexto = new Contexto();
            try
            {

                lista = contexto.Cobros.Where(criterio).ToList();
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
                encontrado = contexto.Cobros.Any(e => e.IdCobros == id);
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
