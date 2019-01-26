using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MyMeetup.Web.Infrastructure
{
    public class Tools
    {
        public static void TransferModalStateError(List<string> modelErrors,ModelStateDictionary modelState)
        {
            foreach (ModelStateEntry error in modelState.Values)
            {
                if (error.ValidationState == ModelValidationState.Invalid)
                {
                    modelErrors.Add(string.Join(",", error.Errors.First().ErrorMessage));
                }
            }
        }
    }
}
