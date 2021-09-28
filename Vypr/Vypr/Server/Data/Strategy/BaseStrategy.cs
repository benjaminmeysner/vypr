// <copyright file="BaseStrategy.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace Vypr.Server.Data.Strategy
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using VyprCore.Interfaces.Logging;
    using VyprCore.Client.Result;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using VyprCore.Interfaces.Repository;
    using VyprCore.Interfaces.Context;
    using VyprCore.Models.Domain;
    using VyprCore.Interfaces.Strategy;
    using VyprCore.Interfaces.Entity;
    using System.Linq;
    using Newtonsoft.Json;
    using VyprCore.Utilities.Extensions;
    using AutoMapper.QueryableExtensions;
    using System.Linq.Dynamic.Core;

    /// <summary>
    /// Base Strategy class in which to inherit.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TEntityViewModel">The type of the entity view model.</typeparam>
    /// <typeparam name="TRepository">The type of the repository.</typeparam>
    /// <seealso cref="Vypr.Server.Interfaces.IStrategy{TEntity, TEntityViewModel, TRepository}" />
    public abstract class BaseStrategy<TEntity, TEntityViewModel, TRepository> : IStrategy<TEntity, TEntityViewModel, TRepository>
        where TEntity : class, IEntity, new()
        where TEntityViewModel : class, IEntityViewModel, new()
        where TRepository : IRepository<TEntity>
    {
        /// <summary>
        /// The repository.
        /// </summary>
        protected IRepository<TEntity> Repository;

        /// <summary>
        /// The mapper.
        /// </summary>
        protected IMapper Mapper;

        /// <summary>
        /// The logger.
        /// </summary>
        protected IVyprLogger Logger;

        /// <summary>
        /// The context
        /// </summary>
        protected IApplicationContext<VyprUser> Context;

        public BaseStrategy(IRepository<TEntity> repository, IMapper mapper, IVyprLogger logger, IApplicationContext<VyprUser> context) : this()
        {
            Repository = repository;
            Mapper = mapper;
            Logger = logger;
            Context = context;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseStrategy{TEntity, TEntityViewModel, TRepository}"/> class.
        /// </summary>
        public BaseStrategy()
        {
            // todo : hp do we needs this?
        }

        public virtual async Task<TEntityViewModel> Add(TEntityViewModel viewModel)
        {
            var entity = Mapper.Map<TEntity>(viewModel);
            var savedEntity = await Repository.Add(entity);

            await Repository.SaveChanges();
            var newViewModel = Mapper.Map<TEntityViewModel>(savedEntity);

            Logger?.File.Trace($"Method [Add] called on Strategy with the following view model type [{viewModel.GetType().FullName}] Model Details [{Newtonsoft.Json.JsonConvert.SerializeObject(newViewModel, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, ReferenceLoopHandling = ReferenceLoopHandling.Ignore })})");
            return newViewModel;
        }

        public virtual async Task AddRange(IEnumerable<TEntityViewModel> viewModels)
        {
            var entites = Mapper.Map<IEnumerable<TEntityViewModel>, IEnumerable<TEntity>>(viewModels);
            await Repository.AddRange(entites);
            await Repository.SaveChanges();
            Logger?.File.Trace($"Method [AddRange] called on Strategy with the following view model type [{typeof(TEntityViewModel).GetType().FullName}] Model Details [{JsonConvert.SerializeObject(viewModels, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, ReferenceLoopHandling = ReferenceLoopHandling.Ignore })})");
        }

        public virtual async Task Delete(int id)
        {
            var entity = await Repository.Delete(id);
            await Repository.SaveChanges();
            Logger?.File.Trace($"Method [Delete] called on Strategy with the following view model type [{typeof(TEntityViewModel).GetType().FullName}] Model Details [{JsonConvert.SerializeObject(entity, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, ReferenceLoopHandling = ReferenceLoopHandling.Ignore })})");
        }

        public virtual async Task<IEnumerable<TEntityViewModel>> Find(Expression<Func<TEntity, bool>> expression)
        {
            try
            {
                return await Task.FromResult(Mapper.ProjectTo<TEntityViewModel>(await Repository.Find(expression)));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public virtual async Task<TEntityViewModel> Get(int id)
        {
            var entity = await Repository.Get(id);
            return await Task.FromResult(Mapper.Map<TEntityViewModel>(entity));
        }

        public virtual async Task<IEnumerable<TEntityViewModel>> GetAll()
        {
            return await Task.FromResult(Mapper.ProjectTo<TEntityViewModel>(await Repository.GetAll()));
        }

        public virtual async Task RemoveRange(IEnumerable<TEntityViewModel> viewModels)
        {
            if (!AssertModelCollectionValid(viewModels))
            {
                return;
            }

            var entities = await viewModels.SelectAsync(async x => await Repository.Get(x.Id.Value));
            foreach (var x in entities.ToList())
            {
                await Repository.Delete(x.Id);
            }
            ////await Repository.RemoveRange(entities);
            await Repository.SaveChanges();
            Logger?.File.Trace($"Method [RemoveRange] called on Strategy with the following view model type [{typeof(TEntityViewModel).GetType().FullName}] Model Details [{JsonConvert.SerializeObject(viewModels, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, ReferenceLoopHandling = ReferenceLoopHandling.Ignore })})");
        }

        public virtual async Task<TEntityViewModel> Update(TEntityViewModel viewModel)
        {
            var entity = Mapper.Map(viewModel, await Repository.Get(viewModel.Id.Value));
            var savedEntity = await Repository.Update(entity);

            await Repository.SaveChanges();

            return Mapper.Map<TEntityViewModel>(savedEntity);
        }

        public virtual async Task UpdateRange(IEnumerable<TEntityViewModel> viewModels)
        {
            if (!AssertModelCollectionValid(viewModels))
            {
                return;
            }

            var entities = await viewModels.SelectAsync(async x => Mapper.Map(x, await Repository.Get(x.Id.Value)));
            await Repository.UpdateRange(entities);
            await Repository.SaveChanges();
            Logger?.File.Trace($"Method [UpdateRange] called on Strategy with the following view model type [{typeof(TEntityViewModel).GetType().FullName}] Model Details [{JsonConvert.SerializeObject(viewModels, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, ReferenceLoopHandling = ReferenceLoopHandling.Ignore })})");
        }

        public virtual async Task<IQueryResult<TEntityViewModel>> Query(string filter = null, string orderBy = null, int? skip = null, int? top = null)
        {
            var data = await Repository.Query(filter, orderBy, skip, top).ProjectTo<TEntityViewModel>(Mapper.ConfigurationProvider).ToListAsync();
            var count = await Repository.Count();

            return new BaseQueryResult<TEntityViewModel>
            {
                Count = count,
                Data = data,
                IsFiltered = !string.IsNullOrEmpty(filter)
            };
        }

        public virtual async Task<IQueryResult<TEntityViewModel>> Query(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> orderBy, int? skip, int? take)
        {
            var data = await Mapper.ProjectTo<TEntityViewModel>(Repository.Query(filter, orderBy, skip, take)).ToListAsync();
            var count = await Repository.Count();

            return new BaseQueryResult<TEntityViewModel>
            {
                Count = count,
                Data = data,
                IsFiltered = !(filter is null)
            };
        }

        public virtual (List<TEntityViewModel> Data, int Count) QueryAsViewModel(IQueryable<TEntityViewModel> viewModels, string filter = null, string orderBy = null, int? skip = null, int? top = null)
        {
            var count = viewModels.Count();
            if (!string.IsNullOrEmpty(filter))
            {
                viewModels = viewModels.Where(filter);
                count = viewModels.Count();
            }
            if (!string.IsNullOrEmpty(orderBy))
            {
                viewModels = viewModels.OrderBy(orderBy);
            }
            if (skip != null)
            {
                viewModels = viewModels.Skip(skip.Value);
            }
            if (top != null)
            {
                viewModels = viewModels.Take(top.Value);
            }
            var finalData = viewModels.ToList();
            return (finalData, count);
        }

        public virtual (List<TEntityViewModel> Data, int Count) QueryAsViewModel(IEnumerable<TEntityViewModel> viewModels, string filter = null, string orderBy = null, int? skip = null, int? top = null)
        {
            var data = viewModels.AsQueryable();
            return QueryAsViewModel(data, filter, orderBy, skip, top);
        }

        public virtual async Task<IEnumerable<TEntityViewModel>> GetAllWithoutQueryFiltering()
        {
            return await Task.FromResult(Mapper.ProjectTo<TEntityViewModel>(await Repository.GetAllWithoutQueryFiltering()));
        }

        public virtual async Task<IEnumerable<TEntityViewModel>> FindWithoutQueryFiltering(Expression<Func<TEntity, bool>> expression)
        {
            try
            {
                return await Task.FromResult(Mapper.ProjectTo<TEntityViewModel>(await Repository.FindWithoutQueryFiltering(expression)));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public virtual async Task<TEntityViewModel> FindSingleWithoutQueryFiltering(Expression<Func<TEntity, bool>> expression)
        {
            try
            {
                return await Task.FromResult(Mapper.Map<TEntityViewModel>(await Repository.FindSingleWithoutQueryFiltering(expression)));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public virtual async Task<IQueryResult<TEntityViewModel>> QueryWithoutQueryFiltering(string filter, string orderBy, int? skip, int? top)
        {
            var data = await Repository.QueryWithoutQueryFiltering(filter, orderBy, skip, top).ProjectTo<TEntityViewModel>(Mapper.ConfigurationProvider).ToListAsync();
            var count = await Repository.CountWithoutQueryFiltering();

            return new BaseQueryResult<TEntityViewModel>
            {
                Count = count,
                Data = data,
                IsFiltered = !string.IsNullOrEmpty(filter)
            };
        }

        public virtual async Task<IQueryResult<TEntityViewModel>> QueryWithoutQueryFiltering(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> orderBy, int? skip, int? top)
        {
            var data = await Mapper.ProjectTo<TEntityViewModel>(Repository.QueryWithoutQueryFiltering(filter, orderBy, skip, top)).ToListAsync();
            var count = await Repository.CountWithoutQueryFiltering();

            return new BaseQueryResult<TEntityViewModel>
            {
                Count = count,
                Data = data,
                IsFiltered = !(filter is null)
            };
        }

        /// <summary>
        /// Asserts the model collection valid.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <returns></returns>
        private bool AssertModelCollectionValid(IEnumerable<TEntityViewModel> collection)
        {
            return !(collection is null) && collection.Any();
        }

        /// <summary>
        /// Counts this instance.
        /// </summary>
        /// <returns></returns>
        public async Task<int> Count()
        {
            return await Repository.Count();
        }
    }
}