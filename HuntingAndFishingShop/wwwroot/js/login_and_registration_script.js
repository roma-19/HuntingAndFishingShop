function hiddenOpen_CloseClick()
{
    let x = document.querySelector(".container-login-registration");
    if (x.style.display === "none")
    {
        x.style.display = "grid";
    }
    else
    {
        x.style.display = "none";
    }
}

document.addEventListener('DOMContentLoaded',  function ()
{
    const signInBtn = document.querySelector('.signin-btn');
    const signUpBtn = document.querySelector('.signup-btn');
    const formBox = document.querySelector('.form-box');
    const block = document.querySelector('.block');

    if (signInBtn && signUpBtn) {
        signUpBtn.addEventListener('click', function() {
            formBox.classList.add('active');
            block.classList.add('active');
        });

        signInBtn.addEventListener('click', function() {
            formBox.classList.remove('active');
            block.classList.remove('active');
        });
    }
    
    document.getElementById("click-to-hide").addEventListener("click", function(){
        hiddenOpen_CloseClick();
        formBox.classList.remove('active');
        block.classList.remove('active');
    });
    document.getElementById("click-to-hide_1").addEventListener("click", function(){
        hiddenOpen_CloseClick();
        formBox.classList.add('active');
        block.classList.add('active');
    });
    document.querySelector(".overlay").addEventListener("click", hiddenOpen_CloseClick);

    
    /*Авторизация*/
    const form_btn_signin = document.querySelector('.form-btn-signin');

    if (form_btn_signin) {
        form_btn_signin.addEventListener('click', function() {
            const requestURL = '/Home/Login';
            const errorContainer = document.getElementById('error-messages-signin');

            const form = {
                email: document.querySelector("#signin_email"),
                password: document.querySelector("#signin_password")
            };

            const body = {
                email: form.email.value,
                password: form.password.value
            };

            sendRequest('POST', requestURL, body)
                .then(data => {
                    cleaningAndClosingForm(form, errorContainer);
                    console.log('Успешный ответ:', data);
                    location.reload()
                })
                .catch(err => {
                    displayErrors(err, errorContainer);
                    console.log(err);
                });
        });
    }
    
    
    /*Регистрация*/
    const form_btn_signup = document.querySelector('.form-btn-signup');

    if (form_btn_signup) {
        form_btn_signup.addEventListener('click', function() {
            const requestURL = '/Home/Register';
            const errorContainer = document.getElementById('error-messages-signup');

            const form = {
                login: document.querySelector("#signup_login"),
                email: document.querySelector("#signup_email"),
                password: document.querySelector("#signup_password"),
                passwordConfirm: document.querySelector("#signup_password_confirm"),
            };

            const body = {
                login: form.login.value,
                email: form.email.value,
                password: form.password.value,
                passwordConfirm: form.passwordConfirm.value,
            };

            sendRequest('POST', requestURL, body)
                .then(data => {
                    cleaningAndClosingForm(form, errorContainer);
                    console.log('Успешный ответ:', data);
                })
                .catch(err => {
                    displayErrors(err, errorContainer);
                    console.log(err);
                });
        });
    }

    function sendRequest(method, url, body) {
        const headers = {
            'Content-Type': 'application/json'
        }
        return fetch(url, {
            method: method,
            body: JSON.stringify(body),
            headers: headers
        }).then(response => {
            if (!response.ok) {
                return response.json().then(errorData => {
                    throw errorData;
                });
            }
            return response.json;
        })
    }

    function displayErrors(errors, errorContainer) {
        errorContainer.innerHTML = '';
        errors.forEach(error => {
            const errorMessage = document.createElement('div');
            errorMessage.classList.add('error');
            errorMessage.textContent = error;
            errorContainer.appendChild(errorMessage);
        });
    }

    function cleaningAndClosingForm(form, errorContainer) {
        errorContainer.innerHTML = '';
        for (const key in form) {
            if (form.hasOwnProperty(key)) {
                form[key].value = '';
            }
        }
        hiddenOpen_CloseClick();
    }

    function toggleMenu()
    {
        const sideMenu = document.getElementById('side-menu');
        sideMenu.classList.toggle('active');
    }

    document.getElementById('hamburger').addEventListener('click', toggleMenu);
    
    document.getElementById("side-menu-button1").addEventListener('click', function () {
        hiddenOpen_CloseClick();
        formBox.classList.add('active');
        block.classList.add('active');
    });
    document.getElementById("side-menu-button2").addEventListener('click', function () {
        hiddenOpen_CloseClick();
        formBox.classList.remove('active');
        block.classList.remove('active');
    });
});




