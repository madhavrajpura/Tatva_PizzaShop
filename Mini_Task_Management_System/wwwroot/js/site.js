// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const togglePassword = document.querySelector('#togglePassword');
const password = document.querySelector('#pass-inp');

if (togglePassword) {
    togglePassword.addEventListener('click', () => {

        const type = password
            .getAttribute('type') === 'password' ?
            'text' : 'password';
        password.setAttribute('type', type);
        togglePassword.classList.toggle('bi-eye-fill');
        togglePassword.classList.toggle('bi-eye-slash-fill');
    });
}

const registerTogglePassword = document.querySelector('#registerTogglePassword');
const registerPassword = document.querySelector('#register-pass-inp');

if (registerTogglePassword) {
    registerTogglePassword.addEventListener('click', () => {

        const type = registerPassword
            .getAttribute('type') === 'password' ?
            'text' : 'password';
        registerPassword.setAttribute('type', type);
        registerTogglePassword.classList.toggle('bi-eye-fill');
        registerTogglePassword.classList.toggle('bi-eye-slash-fill');
    });
}

const toggleConfirmPassword = document.querySelector('#toggleConfirmPassword');
const confirmPassword = document.querySelector('#confirm-pass-inp');

if (toggleConfirmPassword) {
    toggleConfirmPassword.addEventListener('click', () => {

        const type = confirmPassword
            .getAttribute('type') === 'password' ?
            'text' : 'password';
        confirmPassword.setAttribute('type', type);
        toggleConfirmPassword.classList.toggle('bi-eye-fill');
        toggleConfirmPassword.classList.toggle('bi-eye-slash-fill');
    });
}





