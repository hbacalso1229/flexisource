using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Text.RegularExpressions;

namespace FitnessTracker.API.Common.ProducesResponseType
{
    public class ProduceResponseTypeModelProvider : IApplicationModelProvider
    {
        public int Order => 3;

        public void OnProvidersExecuted(ApplicationModelProviderContext context)
        {
        }

        public void OnProvidersExecuting(ApplicationModelProviderContext context)
        {
            foreach (ControllerModel controller in context.Result.Controllers)
            {
                foreach (ActionModel action in controller.Actions)
                {
                    Type returnType = null;
                    if (action.ActionMethod.ReturnType.GenericTypeArguments.Any())
                    {
                        if (action.ActionMethod.ReturnType.GenericTypeArguments[0].GetGenericArguments().Any())
                        {
                            returnType = action.ActionMethod.ReturnType.GenericTypeArguments[0].GetGenericArguments()[0];
                        }
                    }

                    var methodVerbs = action.Attributes.OfType<HttpMethodAttribute>().SelectMany(x => x.HttpMethods).Distinct();
                    string? methodTemplate = action.Attributes.OfType<HttpMethodAttribute>().Select(x => x.Template).FirstOrDefault();
                    bool actionParametersExist = action.Parameters.Any();

                    AddUniversalStatusCodes(action, returnType);

                    if (actionParametersExist == true && new Regex(@"[\{.*\}]").IsMatch(methodTemplate ?? String.Empty) && !methodVerbs.Contains("POST"))
                    {
                        AddProducesResponseTypeAttribute(action, typeof(NotFoundResponse), StatusCodes.Status404NotFound);
                    }
                    if (methodVerbs.Contains("POST"))
                    {
                        AddPostStatusCodes(action, returnType, actionParametersExist);
                    }
                    if (!methodVerbs.Contains("POST"))
                    {
                        AddProducesResponseTypeAttribute(action, returnType, StatusCodes.Status200OK);
                    }
                    if (methodVerbs.Contains("PUT"))
                    {
                        AddPostAndUpdateStatusCodes(action, returnType, actionParametersExist);
                    }
                }
            }
        }

        public void AddProducesResponseTypeAttribute(ActionModel action, Type returnType, int statusCodeResult)
        {
            if (returnType != null)
            {
                action.Filters.Add(new ProducesResponseTypeAttribute(returnType, statusCodeResult));
            }
            else if (returnType == null)
            {
                action.Filters.Add(new ProducesResponseTypeAttribute(statusCodeResult));
            }
        }

        public void AddUniversalStatusCodes(ActionModel action, Type returnType)
        {
            AddProducesResponseTypeAttribute(action, null, StatusCodes.Status401Unauthorized);
            AddProducesResponseTypeAttribute(action, null, StatusCodes.Status403Forbidden);
            AddProducesResponseTypeAttribute(action, typeof(InternalServerErrorResponse), StatusCodes.Status500InternalServerError);
        }

        public void AddPostStatusCodes(ActionModel action, Type returnType, bool actionParametersExist)
        {
            AddProducesResponseTypeAttribute(action, returnType, StatusCodes.Status201Created);
            AddPostAndUpdateStatusCodes(action, returnType, actionParametersExist);

            if (actionParametersExist == false)
            {
                AddProducesResponseTypeAttribute(action, typeof(NotFoundResponse), StatusCodes.Status404NotFound);
            }
        }

        public void AddPostAndUpdateStatusCodes(ActionModel action, Type returnType, bool actionParametersExist)
        {
            AddProducesResponseTypeAttribute(action, typeof(BadRequestResponse), StatusCodes.Status400BadRequest);
            AddProducesResponseTypeAttribute(action, typeof(UnprocessableEntityResponse), StatusCodes.Status422UnprocessableEntity);
        }
    } 
}
