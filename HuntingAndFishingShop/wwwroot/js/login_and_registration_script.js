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
});


