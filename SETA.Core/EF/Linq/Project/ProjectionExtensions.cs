﻿using System.Linq;

namespace SETA.Core.EF.Linq.Project
{
    public static class ProjectionExtensions
    {
        public static IProjectionExpression<TSource> Project<TSource>(this IQueryable<TSource> source)
        {
            return new ProjectionExpression<TSource>(source);
        }
    }
}
