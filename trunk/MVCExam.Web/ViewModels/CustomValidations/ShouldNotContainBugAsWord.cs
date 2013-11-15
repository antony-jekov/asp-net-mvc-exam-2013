using System.ComponentModel.DataAnnotations;

namespace MVCExam.Web.ViewModels.CustomValidations
{
    public class ShouldNotContainBugAsWord : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string valueAsString = value as string;

            if (!string.IsNullOrEmpty(valueAsString))
            {
                if (valueAsString.ToLower().Contains("bug"))
                {
                    return false;
                }
            }

            return true;
        }
    }
}