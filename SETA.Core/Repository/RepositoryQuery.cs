using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SETA.Core.Repository
{
    public sealed class RepositoryQuery<TEntity> where TEntity : class
    {
        private readonly List<Expression<Func<TEntity, object>>>
            _includeProperties;

        private readonly Repository<TEntity> _repository;
        private Expression<Func<TEntity, bool>> _filter;
        private Func<IQueryable<TEntity>,
            IOrderedQueryable<TEntity>> _orderByQuerable;
        private int? _page;
        private int? _pageSize;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryQuery{TEntity}"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public RepositoryQuery(Repository<TEntity> repository)
        {
            _repository = repository;
            _includeProperties =
                new List<Expression<Func<TEntity, object>>>();
        }

        /// <summary>
        /// Filters the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public RepositoryQuery<TEntity> Filter(
            Expression<Func<TEntity, bool>> filter)
        {
            _filter = filter;
            return this;
        }

        /// <summary>
        /// Orders the by.
        /// </summary>
        /// <param name="orderBy">The order by.</param>
        /// <returns></returns>
        public RepositoryQuery<TEntity> OrderBy(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
        {
            _orderByQuerable = orderBy;
            return this;
        }

        /// <summary>
        /// Includes the specified expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        public RepositoryQuery<TEntity> Include(
            Expression<Func<TEntity, object>> expression)
        {
            _includeProperties.Add(expression);
            return this;
        }

        /// <summary>
        /// Gets the page.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalCount">The total count.</param>
        /// <returns></returns>
        public IEnumerable<TEntity> GetPage(
            int page, int pageSize, out int totalCount)
        {
            _page = page;
            _pageSize = pageSize;
            totalCount = _repository.Get(_filter).Count();

            return _repository.Get(
                _filter,
                _orderByQuerable, _includeProperties, _page, _pageSize);
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TEntity> Get()
        {
            return _repository.Get(
                _filter,
                _orderByQuerable, _includeProperties, _page, _pageSize);
        }

        /// <summary>
        /// Shows the query.
        /// </summary>
        /// <returns></returns>
        public string ShowQuery()
        {
            return _repository.Get(
                _filter,
                _orderByQuerable, _includeProperties, _page, _pageSize).ToString();
        }
    }
}
