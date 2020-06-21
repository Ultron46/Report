using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SourceControlAssignment.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class FileExtensionsAttribute : ValidationAttribute
    {
        private List<string> AllowedExtensions { get; set; }

        public FileExtensionsAttribute(string Extensions)
        {
            AllowedExtensions = Extensions.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        public override bool IsValid(object value)
        {
            HttpPostedFileBase file = value as HttpPostedFileBase;

            if (file != null)
            {
                var fileName = file.FileName;

                return AllowedExtensions.Any(y => fileName.EndsWith(y));
            }

            return true;
        }
    }
}