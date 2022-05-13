using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Security.Cryptography;
using Plantjes.Models;
using Plantjes.Models.Db;
using Microsoft.EntityFrameworkCore;
using System;
using Plantjes.Models.Enums;
/*comments kenny*/

namespace Plantjes.Dao
{
    public abstract class DaoBase
    {
        protected static readonly plantenContext context = new plantenContext();

        #region Get methods
        //written by Warre
        /// <summary>
        /// Gets a list of type <see cref="DbSet{TEntity}"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the list.</typeparam>
        /// <param name="distinct">Boolean which decides if list must be distinct.</param>
        /// <returns>Returns a <see cref="IEnumerable{T}"/> of type <see cref="DbSet{TEntity}"/>.</returns>
        public static IEnumerable<TEntity> GetList<TEntity>(bool distinct = false) where TEntity : class
        {
            var dbset = context.Set<TEntity>().ToList();

            return distinct ? dbset.Distinct() : dbset;
        }

        //written by Warre
        /// <summary>
        /// <seealso cref="GetList{TEntity}(bool)"/> with a where predicate.
        /// </summary>
        /// <typeparam name="TEntity">The type of the list.</typeparam>
        /// <param name="predicate">The requirement of the where.</param>
        /// <param name="distinct"><see cref="GetList{TEntity}(bool)"/>.</param>
        /// <returns>Returns of a <see cref="IEnumerable{T}"/> of type <see cref="DbSet{TEntity}"/>.</returns>
        public static IEnumerable<TEntity> GetListWhere<TEntity>(Func<TEntity, bool> predicate, bool distinct = false) where TEntity : class
        {
            return GetList<TEntity>(distinct).Where(predicate);
        }
        #endregion
    }
}
