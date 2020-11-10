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
        public static bool Guardar(Cobros cobro)
        {
            return Insertar(cobro);
        }

        private static bool Insertar(Cobros cobros)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                foreach (var item in cobros.cobrosDetalle)
                {
                    item.Venta = contexto.Ventas.Find(item.VentaId);
                    item.Venta.Balance -= item.Cobrado;
                    contexto.Entry(item.Venta).State = EntityState.Modified;
                }
                contexto.Cobros.Add(cobros);
                paso = (contexto.SaveChanges() > 0);
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
                var cobro = Buscar(id);

                contexto.Entry(cobro).State = EntityState.Deleted;
                paso = (contexto.SaveChanges() > 0);
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

        
    }
}
