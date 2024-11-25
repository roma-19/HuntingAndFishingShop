document.addEventListener('DOMContentLoaded',  function ()
{
    //Открытие и закрытие контейнера (отмена скролла при открытии)
    function hiddenOpen_CloseClick()
    {
        let x = document.querySelector(".container-login-registration");
        if (x.style.display === "none")
        {
            x.style.display = "grid";
            document.body.classList.add("noscroll");
        }
        else
        {
            x.style.display = "none";
            document.body.classList.remove("noscroll");
        }
    }
    
    
    //Переключение между формами входа и регистрации
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
    
    
    //Кнопка регистрации в форме
    const reg_button = document.getElementById("reg");
    if (reg_button) {
        reg_button.addEventListener('click', function() {
            hiddenOpen_CloseClick();
            formBox.classList.add('active');
            block.classList.add('active');
        });
    }
    
    
    //Кнопка входа в форме
    const log_button = document.getElementById("log");
    if (log_button) {
        log_button.addEventListener('click', function() {
            hiddenOpen_CloseClick();
            formBox.classList.remove('active');
            block.classList.remove('active');
        });
    }
    
    
    //Закрытие формы при нажатии за ее пределы
    document.querySelector(".overlay").addEventListener("click", hiddenOpen_CloseClick);
    
    
    /*Авторизация*/
    const form_btn_signin = document.querySelector('.form-btn-signin');

    if (form_btn_signin) {
        form_btn_signin.addEventListener('click', function() {
            
            //URL и контейнер для ошибок
            const requestURL = '/Home/Login';
            const errorContainer = document.getElementById('error-messages-signin');
        
            //Сбор данных формы
            const form = {
                email: document.querySelector("#signin_email"),
                password: document.querySelector("#signin_password")
            };
            const body = {
                email: form.email.value,
                password: form.password.value
            };

            //Отправка запроса
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

            //URL и контейнер для ошибок
            const requestURL = '/Home/Register';
            const errorContainer = document.getElementById('error-messages-signup');

            //Сбор данных формы
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

            //Отправка запроса
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

    
    //Отправление HTTP-запроса на указанный URL с заданным методом и телом запроса
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

    
    //Отображение списка ошибок
    function displayErrors(errors, errorContainer) {
        errorContainer.innerHTML = '';
        errors.forEach(error => {
            const errorMessage = document.createElement('div');
            errorMessage.classList.add('error');
            errorMessage.textContent = error;
            errorContainer.appendChild(errorMessage);
        });
    }

    
    //очистка и закрытие формы
    function cleaningAndClosingForm(form, errorContainer) {
        errorContainer.innerHTML = '';
        for (const key in form) {
            if (form.hasOwnProperty(key)) {
                form[key].value = '';
            }
        }
        hiddenOpen_CloseClick();
    }

    
    //Отображение альтернативного меню
    function toggleMenu()
    {
        const sideMenu = document.getElementById('side-menu');
        sideMenu.classList.toggle('active');
    }

    
    //Обработчик открытия меню
    document.getElementById('hamburger').addEventListener('click', toggleMenu);
    
    
    //Кнопка регистрации в альтернативном меню
    document.getElementById("side-menu-button1").addEventListener('click', function () {
        hiddenOpen_CloseClick();
        formBox.classList.add('active');
        block.classList.add('active');
    });
    
    
    //Кнопка входа в альтернативном меню
    document.getElementById("side-menu-button2").addEventListener('click', function () {
        hiddenOpen_CloseClick();
        formBox.classList.remove('active');
        block.classList.remove('active');
    });
});




