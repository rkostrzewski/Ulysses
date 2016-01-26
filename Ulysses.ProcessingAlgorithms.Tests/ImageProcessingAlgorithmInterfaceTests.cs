using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using Ulysses.ProcessingAlgorithms.Templates;

namespace Ulysses.ProcessingAlgorithms.Tests
{
    [TestFixture]
    public class ImageProcessingAlgorithmInterfaceTests
    {
        [Test]
        public void AllImageProcessingAlgorithmsShouldContainConstructorWithIImageProcessingAlgorithmTemplate()
        {
            // Given

            var iImageProcessingAlgorithmType = typeof (IImageProcessingAlgorithm);
            var iImageProcessingAlgorithmTemplateType = typeof(IImageProcessingAlgorithmTemplate);

            // When

            var classesThatImplementTheInterface = iImageProcessingAlgorithmType.Assembly.GetTypes()
                                 .Where(t => iImageProcessingAlgorithmType.IsAssignableFrom(t))
                                 .Where(t => t.IsClass && t.IsPublic && !t.IsAbstract);

            var classesConstructorsThatTakeImplementationOfIImageProcessingAlgorithmType = classesThatImplementTheInterface.Select(c => c.GetConstructors()).Select(cc => cc.Where(c =>
            {
                var constructor = c;
                var constructorParameters = constructor.GetParameters();
                if (constructorParameters.Length != 1)
                {
                    return false;
                }

                var parameterType = constructorParameters.First().ParameterType;
                
                return iImageProcessingAlgorithmTemplateType.IsAssignableFrom(parameterType) && parameterType.IsClass && parameterType.IsPublic && !parameterType.IsAbstract;
            }).FirstOrDefault());

            // Then

            CollectionAssert.AllItemsAreNotNull(classesConstructorsThatTakeImplementationOfIImageProcessingAlgorithmType);
        }
    }
}
