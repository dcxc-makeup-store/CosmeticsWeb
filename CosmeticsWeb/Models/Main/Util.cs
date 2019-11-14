using CosmeticsWeb.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace CosmeticsWeb.Models.Main
{
    public class Util
    {
        /// <summary>
        /// 返回所有的化妆品类型
        /// </summary>
        /// <returns></returns>
        
        public static List<string> AllCosmeticTypes()
        {
            var da = new CosmeticsEntities();
            return da.商品类型表.Select(m => m.商品类型名称).ToList();

        }
        ///<summary>
        /// Copies the data of one object to another. The target object 'pulls' properties of the first.
        /// This any matching properties are written to the target.
        ///
        /// The object copy is a shallow copy only. Any nested types will be copied as
        /// whole values rather than individual property assignments (ie. via assignment)
        /// </summary>
        /// <param name="source">The source object to copy from</param>
        /// <param name="target">The object to copy to</param>
        /// <param name="excludedProperties">A comma delimited list of properties that should not be copied</param>
        public static void CopyObjectData(object source, object target, string excludedProperties)
        {
            //string[] excluded = null;
            var excluded = new List<string>();
            if (!string.IsNullOrEmpty(excludedProperties))
                excluded = excludedProperties.Split(new char[1] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            var miT = target.GetType().GetMembers();
            foreach (var field in miT.Where(m => m.MemberType == MemberTypes.Property || m.MemberType == MemberTypes.Field))
            {
                var name = field.Name;
                // Skip over any property exceptions
                if (!string.IsNullOrEmpty(excludedProperties) && excluded.Contains(name))
                    continue;
                if (field.MemberType == MemberTypes.Field)
                {
                    var sourceField = source.GetType().GetField(name);
                    if (sourceField == null)
                        continue;
                    var sourceValue = sourceField.GetValue(source);
                    ((FieldInfo)field).SetValue(target, sourceValue);
                }
                else if (field.MemberType == MemberTypes.Property)
                {
                    var piTarget = field as PropertyInfo;
                    var sourceField = source.GetType().GetProperty(name);
                    if (sourceField == null)
                        continue;
                    if (piTarget.CanWrite && sourceField.CanRead)
                    {
                        var sourceValue = sourceField.GetValue(source, null);
                        piTarget.SetValue(target, sourceValue, null);
                    }
                }
            }
        }


        public static string ConvertMoneyToString(decimal money)
        {
            return String.Format("{0:#,##0.00}", money);
        }
    }
}