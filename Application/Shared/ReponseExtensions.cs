using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;

namespace Application.Shared
{
    public static class ReponseExtensions
    {
        public static Response<T> Ok<T>()
        {
            return new Response<T>()
            {
                Success = true
            };
        }
        public static Response<T> Ok<T>(T t)
        {
            return new Response<T>()
            {
                Success = true,
                Data = t
            };
        }
        public static Response<T> Fail<T>()
        {
            return new Response<T>()
            {
                Success = false,
            };
        }
        public static Response<T> Fail<T>(ErrorCode errorCode)
        {
            return new Response<T>()
            {
                Success = false,
                ErrorCode = errorCode
            };
        }
        public static Response<T> Fail<T>(string error)
        {
            return new Response<T>()
            {
                Success = false,
                Errors = new List<string>() { error }
            };
        }
        public static Response<T> Fail<T>(ErrorCode errorCode, string error)
        {
            return new Response<T>()
            {
                Success = false,
                ErrorCode = errorCode,
                Errors = new List<string>() { error }
            };
        }
        public static Response<T> Fail<T>(IEnumerable<string> errors)
        {
            return new Response<T>()
            {
                Success = false,
                Errors = errors
            };
        }
        public static Response<T> Fail<T>(IdentityResult result)
        {
            return new Response<T>()
            {
                Success = false,
                Errors = result.Errors.Select(x => x.Description)
            };
        }
        public static Response<T> Fail<T>(ErrorCode errorCode, IdentityResult result)
        {
            return new Response<T>()
            {
                Success = false,
                ErrorCode = errorCode,
                Errors = result.Errors.Select(x => x.Description)
            };
        }
        public static Response<T> Fail<T>(ValidationResult result)
        {
            return new Response<T>()
            {
                Success = false,
                Errors = result.Errors.Select(x => x.ErrorMessage)
            };
        }
        public static Response<T> Fail<T>(ErrorCode errorCode, ValidationResult result)
        {
            return new Response<T>()
            {
                Success = false,
                ErrorCode = errorCode,
                Errors = result.Errors.Select(x => x.ErrorMessage)
            };
        }
    }
}