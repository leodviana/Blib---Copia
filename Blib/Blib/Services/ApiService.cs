using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Blib.Models;
using Newtonsoft.Json;

namespace Blib.Services
{
    public class ApiService
    {
        public async Task<Response> Login(string email, string password)
        {
            try
            {
                var loginRequest = new LoginRequest
                {
                    Email = email,
                    Password = password
                };

                var jsonRequest = JsonConvert.SerializeObject(loginRequest);
                var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://www.painelstudio.com");
                var url = "/ApiBlib/api/Usuario/Login";
                var response = await client.PostAsync(url, httpContent);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Usuário ou Senha Incorretos",

                    };

                }

                var result = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<usuario>(result);

                return new Response
                {
                    IsSuccess = true,
                    Message = "login Ok",
                    Result = user
                };

            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
                throw;
            }
        }
         public async Task<Response> Gravaposicao(usuario customer)
        {
            try
            {

                var jsonRequest = JsonConvert.SerializeObject(customer);
                var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://www.painelstudio.com");
                var url = "/ApiBlib/api/Usuario/Gravaposicao";
                var response = await client.PostAsync(url, httpContent);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString()

                    };

                }

                var result = await response.Content.ReadAsStringAsync();
                var NewCustomer = JsonConvert.DeserializeObject<usuario>(result);

                return new Response
                {
                    IsSuccess = true,
                    Message = "Usuario Salvo Ok",
                    Result = NewCustomer
                };

            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
                throw;
            }
        }


        public async Task<Response> Gravausuario(usuario customer)
        {
            try
            {
                var httpContent = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");

                var client = new HttpClient();
                
                client.BaseAddress = new Uri("http://www.painelstudio.com");
                //http://localhost:56345/api/Usuario/GravaUsuario
                  var url = "/ApiBlib/api/Usuario/GravaUsuario";
              //  var url = "/api/Usuario/GravaUsuario";
                // www.painelstudio.com / ApiBlib / api / Usuario / GravaUsuario
                //var url = "ApiBlib/api/Usuario/Usuarioscomposicao";
                var response = await client.PostAsync(url, httpContent);


                if (!response.IsSuccessStatusCode)
                {
                    var teste = response.Content.ReadAsStringAsync().Result;
                    
                    
                    var mensagem  = JsonConvert.DeserializeObject<retorno>(teste);

                    return new Response
                    {
                        IsSuccess = false,
                        Message = mensagem.Message//"Email já cadastrado"

                    };

                }


                var result = await response.Content.ReadAsStringAsync();
                var NewCustomer = JsonConvert.DeserializeObject<usuario>(result);

                
                return new Response
                {
                    IsSuccess = true,
                    Message = "Usuario Salvo com Sucesso!",
                    Result = NewCustomer
                };

            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
                throw;
            }

            
        }


        public async Task<List<usuario>> Usuarioscomposicao()
        {
            try
            {

                //var jsonRequest = JsonConvert.SerializeObject(customer);
              //  var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://www.painelstudio.com");
                var url = "ApiBlib/api/Usuario/Usuarioscomposicao";
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        return null;

                    }

                }

                var result = await response.Content.ReadAsStringAsync();
                var usuariocomposicao = JsonConvert.DeserializeObject<List<usuario>>(result);

                return usuariocomposicao;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
