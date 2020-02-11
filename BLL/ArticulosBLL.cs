using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using PrimerParcialAplicada.Entidades;
using PrimerParcialAplicada.DAL;

namespace PrimerParcialAplicada.BLL
{
    public class ArticulosBLL
    {
        public static bool Guardar(Articulos articulo)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                if (db.Articulos.Add(articulo) != null)
                    paso = db.SaveChanges() > 0;
            }
            catch(Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }

        public static bool Modificar(Articulos articulo)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                db.Entry(articulo).State = EntityState.Modified;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }
        public static bool Eliminar(int id)
        {
            Contexto db = new Contexto();
            bool paso = false;

            try
            {
                var eliminar = db.Articulos.Find(id);
                db.Entry(eliminar).State = EntityState.Deleted;
            }
            catch
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }
        public static Articulos Buscar(int id)
        {
            Contexto db = new Contexto();
            Articulos persona = new Articulos();
            try
            {
                persona = db.Articulos.Find(id);
            }
            catch
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return persona;
        }

        public static List<Articulos> GetList(Expression<Func<Articulos, bool>> articulo)
        {
            List<Articulos> lista = new List<Articulos>();
            Contexto db = new Contexto();
            try
            {
                lista = db.Articulos.Where(articulo).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return lista;
        }
    }
}
