using Microsoft.AspNetCore.Identity;
using User_Authentication_System.constnet;
using User_Authentication_System.Helpers;
using User_Authentication_System.View_Models;

namespace User_Authentication_System.Repos.AuthRepos
{
    public class AuthRepo : IAuthRepo
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthRepo(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<bool> AllowUser(string user)
        {
            var Isexist = await _userManager.FindByNameAsync(user);

            return Isexist is not null ? false : true;
        }

        public async Task<bool> AllowEmail(string email)
        {
            var Isexist = await _userManager.FindByEmailAsync(email);

            return Isexist is not null ? false : true;
        }

        public async Task<ResponseData<UserLoginVM>> LoginAsync(UserLoginVM model)
        {
            try
            {
                var user = _userManager.FindByEmailAsync(model.Email);

                if (user == null)
                {
                    return new ResponseData<UserLoginVM>

                    {
                        Data = new UserLoginVM()
                        {
                            Email = model.Email,
                            Password = model.Password,
                        },
                        Errors = Errors.UnVaildUserOrPassword
                    };

                }



                var userName = model.Email.Split('@')[0];
                var res = await _signInManager.PasswordSignInAsync(userName, model.Password, true, false);

                if (res.Succeeded)
                {
                    return new ResponseData<UserLoginVM>()
                    {
                        Data = new UserLoginVM()
                        {
                            Email = model.Email,
                            Password = model.Password,
                        }
                    };
                }

                return new ResponseData<UserLoginVM>

                {
                    Data = new UserLoginVM()
                    {
                        Email = model.Email,
                        Password = model.Password,
                    },
                    Errors = Errors.UnVaildUserOrPassword
                };
            }
            catch (Exception)
            {

                return new ResponseData<UserLoginVM>

                {
                    Data = new UserLoginVM()
                    {
                        Email = model.Email,
                        Password = model.Password,
                    },
                    Errors = Errors.ErrorOcurrd
                };
            }
        }

        public async Task LogOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<ResponseData<UserRegisterVM>> RegisterAsync(UserRegisterVM model)
        {
            try
            {

                var IsExist = await _userManager.FindByEmailAsync(model.Email);
                if (IsExist != null)
                {
                    return new ResponseData<UserRegisterVM>
                    {
                        Data = new UserRegisterVM()
                        {
                            Email = model.Email,
                            Password = model.Password,
                            ConfirmPassword = model.ConfirmPassword,
                            User = model.User,
                        },
                        Errors = Errors.EmailAlredyExist
                    };

                }


                IsExist = await _userManager.FindByNameAsync(model.User);
                if (IsExist != null)
                {
                    return new ResponseData<UserRegisterVM>
                    {
                        Data = new UserRegisterVM()
                        {
                            Email = model.Email,
                            Password = model.Password,
                            ConfirmPassword = model.ConfirmPassword,
                            User = model.User,
                        },
                        Errors = Errors.UserAlredyExist
                    };

                }

                var user = new IdentityUser
                {
                    Email = model.Email,
                    UserName = model.User
                };

                var res = await _userManager.CreateAsync(user, model.Password);

                if (res.Succeeded)
                {
                    return new ResponseData<UserRegisterVM>
                    {
                        Data = new UserRegisterVM()
                        {
                            ConfirmPassword = model.ConfirmPassword,
                            Password = model.Password,
                            Email = model.Email,
                            User = model.User,
                        }

                    };
                }

                var respons = new ResponseData<UserRegisterVM>
                {
                    Data = new UserRegisterVM()
                    {
                        ConfirmPassword = model.ConfirmPassword,
                        Password = model.Password,
                        Email = model.Email,
                        User = model.User
                    },
                   
                };

                foreach (var item in res.Errors)
                {
                    respons.Errors += $"{item.Description}\n";
                }


                return respons;

            }
            catch (Exception)
            {

                return new ResponseData<UserRegisterVM>
                {
                    Data = new UserRegisterVM()
                    {
                        ConfirmPassword = model.ConfirmPassword,
                        Password = model.Password,
                        Email = model.Email,
                        User = model.User
                    },
                    Errors = Errors.ErrorOcurrd
                };
            }
        }
    }
}
