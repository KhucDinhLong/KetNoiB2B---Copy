using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using AutoMapper;
using Dapper;
using SETA.Core.Helper.Extensions;

namespace SETA.Core.Helper.Mapping
{
    public class AutoMapping
    {
        /// <summary>
        /// Map: Auto map object from source => target
        /// <para>(Using AutoMapper)</para>
        /// </summary>
        /// <author>SuNV</author>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns>target</returns>
        public static T2 Map<T1, T2>(T1 source, T2 target)
        {
            try
            {
                Mapper.CreateMap(typeof(T1), typeof(T2));
                target = Mapper.Map(source, target);
                return target;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("operation failed!", ex);
            }
        }
        
        public static T MapLeft<T>(T self, T to)
        {
            try
            {
                if (self != null && to != null)
                {
                    Type type = typeof(T);
                    foreach (System.Reflection.PropertyInfo pi in type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
                    {
                       
                            object selfValue = type.GetProperty(pi.Name).GetValue(self, null);
                            object toValue = type.GetProperty(pi.Name).GetValue(to, null);

//                            if (selfValue != toValue && ((toValue == null && selfValue != null) || (!toValue.Equals(selfValue) && selfValue != null)))
                            if (selfValue != toValue && (toValue == null && selfValue != null))
                            {
                                type.GetProperty(pi.Name).SetValue(to, type.GetProperty(pi.Name).GetValue(self, null));
                            }
                    }
                }
                Mapper.CreateMap(typeof(T), typeof(T));
                self = Mapper.Map(to, self);
                return self;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("operation failed!", ex);
            }
        }
        public static object MapParameters(object oParams)
        {
            var obj = oParams.GetType();
            if (obj.FullName == "Dapper.DynamicParameters")
            {
                var listParams = oParams as DynamicParameters;
                if (listParams != null)
                {
                    var parameters = listParams.parameters;
                    if (parameters.Count > 0)
                    {
                        foreach (var parameter in (oParams as DynamicParameters).parameters)
                        {
                            var value = parameter.Value.Value;
                            if (value !=null && value.GetType().FullName == "System.String")
                            {
                                value = value.ToString().Replace("'", "''");
                            }
                            parameter.Value.Value = value;
                        }
                    }
                }
            }
            return oParams;
        }
    }
}
