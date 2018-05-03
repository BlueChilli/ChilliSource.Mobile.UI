using System;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;
using System.Collections.Generic;

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// A simplified <see cref="DataTemplateSelector"/> that uses <see cref="DataTemplateAttribute"/> to templates to view models
    /// </summary>
    public class AttributeDataTemplateSelector : DataTemplateSelector, IDisposable
    {
        List<Tuple<DataTemplate, Type>> _dataTemplates;

        public AttributeDataTemplateSelector(Type derivedType)
        {
            _dataTemplates = new List<Tuple<DataTemplate, Type>>();


            // Finds any DataTemplateAttribute object
#if NETSTANDARD1_6
            var attributes = derivedType
                    .GetTypeInfo().GetConstructors()
                    .Single(c => c.GetCustomAttributes<DataTemplateAttribute>(false).Any())
                    .GetCustomAttributes<DataTemplateAttribute>(false).
                    ToList();

#else
            var attributes = derivedType
                                   .GetConstructors()
                                   .Single(c => c.GetCustomAttributes<DataTemplateAttribute>(false).Any())
                                   .GetCustomAttributes<DataTemplateAttribute>(false).
                                   ToList();

#endif


            // Creates templates based on attribute properties
            attributes.ForEach((attribute) =>
            {
                _dataTemplates.Add(new Tuple<DataTemplate, Type>(new DataTemplate(attribute.Template), attribute.ViewModel));
            });
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return _dataTemplates.FirstOrDefault((dataTemplate) => item.GetType().GetTypeInfo().IsEquivalentTo(dataTemplate.Item2)).Item1;
        }

        public void Dispose()
        {
            _dataTemplates.Clear();
            _dataTemplates = null;
        }
    }
}