using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;
using Application.Auth.Requests;
using Application.Authors.Models;
using Application.Shared;
using Application.Authors.Requests;
using Application.Authors.Responses;
using System.Net.Http.Headers;
using Application.Books.Model;
using Application.Books.Requests;
using Application.Books.Responses;

namespace MVC.Client
{
    public class HttpClientService
    {
        private readonly HttpClient _httpClient;
        public HttpClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            httpClient.BaseAddress = new Uri("https://localhost:7112/v1/");
        }

        public async Task<Response<string>> Login(LoginRequest model)
        {
            var json = JsonConvert.SerializeObject(model);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync($"auth/login", data);

            return await result.Content.ReadFromJsonAsync<Response<string>>();
        }

        public async Task<Response<string>> Register(RegisterRequest model)
        {
            var json = JsonConvert.SerializeObject(model);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync($"auth/register", data);

            return await result.Content.ReadFromJsonAsync<Response<string>>();
        }

        public async Task<Response<IEnumerable<AuthorModel>>> GetAuthors()
        {
            var result = await _httpClient.GetAsync($"author/getauthors");
            return await result.Content.ReadFromJsonAsync<Response<IEnumerable<AuthorModel>>>();
        }

        public async Task<Response<AuthorModel>> GetAuthorById(string id)
        {
            var result = await _httpClient.GetAsync($"author/{id}");
            return await result.Content.ReadFromJsonAsync<Response<AuthorModel>>();
        }

        public async Task<Response<string>> CreateAuthhor(AddAuthorRequest author)
        {
            var json = JsonConvert.SerializeObject(author);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync($"author/addauthor", data);

            return await result.Content.ReadFromJsonAsync<Response<string>>();
        }

        public async Task<Response<UpdateAuthorResponse>> UpdateAuthor(UpdateAuthorRequest author)
        {
            var json = JsonConvert.SerializeObject(author);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await _httpClient.PutAsync($"author/updateauthor", data);

            return await result.Content.ReadFromJsonAsync<Response<UpdateAuthorResponse>>();
        }

        public async Task<Response<DeleteAuthorResponse>> DeleteAuthor(string id)
        {
            var result = await _httpClient.DeleteAsync($"author/deleteauthor/{id}");
            return await result.Content.ReadFromJsonAsync<Response<DeleteAuthorResponse>>();
        }

        public async Task<Response<IEnumerable<BookModel>>> GetBooks()
        {
            var result = await _httpClient.GetAsync($"book/getbooks");
            return await result.Content.ReadFromJsonAsync<Response<IEnumerable<BookModel>>>();
        }

        public async Task<Response<BookModel>> GetBookById(string id)
        {
            var result = await _httpClient.GetAsync($"book/{id}");
            return await result.Content.ReadFromJsonAsync<Response<BookModel>>();
        }

        public async Task<Response<string>> CreateBook(AddBookRequest book)
        {
            var multipartContent = new MultipartFormDataContent();

            multipartContent.Add(new StringContent(book.Title), "Title");
            multipartContent.Add(new StringContent(book.Description), "Description");
            multipartContent.Add(new StringContent(book.Rating.ToString()), "Rating");
            multipartContent.Add(new StringContent(book.PublishYear.ToString()), "PublishYear");
            multipartContent.Add(new StringContent(book.IsTaken.ToString()), "IsTaken");

            if (book.Image != null)
            {
                var imageContent = new StreamContent(book.Image.OpenReadStream());
                multipartContent.Add(imageContent, "Image", book.Image.FileName);
            }

            if (book.AuthorIds != null)
            {
                foreach (var authorId in book.AuthorIds)
                {
                    multipartContent.Add(new StringContent(authorId), "AuthorIds");
                }
            }

            var result = await _httpClient.PostAsync("book/addbook", multipartContent);

            return await result.Content.ReadFromJsonAsync<Response<string>>();
        }

        public async Task<Response<UpdateAuthorResponse>> UpdateBook(UpdateBookRequest book)
        {
            var multipartContent = new MultipartFormDataContent();

            multipartContent.Add(new StringContent(book.Id), "Id");
            multipartContent.Add(new StringContent(book.Title), "Title");
            multipartContent.Add(new StringContent(book.Description), "Description");
            multipartContent.Add(new StringContent(book.Rating.ToString()), "Rating");
            multipartContent.Add(new StringContent(book.PublishYear.ToString()), "PublishYear");
            multipartContent.Add(new StringContent(book.IsTaken.ToString()), "IsTaken");

            if (book.Image != null)
            {
                var imageContent = new StreamContent(book.Image.OpenReadStream());
                multipartContent.Add(imageContent, "Image", book.Image.FileName);
            }

            if (book.AuthorIds != null)
            {
                foreach (var authorId in book.AuthorIds)
                {
                    multipartContent.Add(new StringContent(authorId), "AuthorIds");
                }
            }

            var result = await _httpClient.PutAsync($"book/updatebook", multipartContent);

            return await result.Content.ReadFromJsonAsync<Response<UpdateAuthorResponse>>();
        }

        public async Task<Response<DeleteBookResponse>> DeleteBook(string id)
        {
            var result = await _httpClient.DeleteAsync($"book/deletebook/{id}");
            return await result.Content.ReadFromJsonAsync<Response<DeleteBookResponse>>();
        }

        //https://localhost:7112/v1/Book/AddBook?Title=ewq&Description=asd&Rating=4&PublishYear=1232&IsTaken=true&AuthorIds=09cc9955-62c9-4d19-9993-b69da92e9896&AuthorIds=e36fabc8-4436-4d65-8954-fd65fde13c6c&AuthorIds=string
    }
}
