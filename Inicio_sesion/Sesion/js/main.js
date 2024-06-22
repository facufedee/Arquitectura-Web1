/*=============== SHOW MENU ===============*/
const navMenu = document.getElementById('nav-menu'),
      navToggle = document.getElementById('nav-toggle'),
      navClose = document.getElementById('nav-close')

/* Menu show */
if(navToggle){
   navToggle.addEventListener('click', () =>{
      navMenu.classList.add('show-menu')
   })
}

/* Menu hidden */
if(navClose){
   navClose.addEventListener('click', () =>{
      navMenu.classList.remove('show-menu')
   })
}

var acc = document.getElementsByClassName("accordion");
var i;

for (i = 0; i < acc.length; i++) {
  acc[i].addEventListener("click", function() {
    this.classList.toggle("active");
    var panel = this.nextElementSibling;
    if (panel.style.display === "block") {
      panel.style.display = "none";
    } else {
      panel.style.display = "block";
    }
  });
}


function abrir(link) { // Modificamos la función para recibir el enlace
  const modal = document.getElementById("vent");
  const contenido = document.getElementById("ventana-contenido");
  const text = link.getAttribute("data-content"); // Obtenemos el contenido del atributo
  contenido.textContent = text; // Cambiamos el contenido del modal
  modal.style.display = "block"; // Mostramos el modal
}

function cerrar() {
  document.getElementById("vent").style.display = "none"; // Ocultamos el modal
}

document.addEventListener('DOMContentLoaded', function () {
  document.querySelectorAll('.open-modal').forEach(function (link) { // Configuramos el evento
     link.addEventListener('click', function (e) {
        e.preventDefault(); // Evitar la navegación
        abrir(this); // Abrir el modal con el contenido adecuado
     });
  });
});


document.getElementById("loginForm").addEventListener("submit", function (event) {
    var username = document.getElementById("username").value.trim();
    var password = document.getElementById("password").value.trim();
    var errorMessage = document.getElementById("errorMessage");

    if (username === "" || password === "") {
        errorMessage.innerHTML = "Por favor, complete todos los campos.";
        errorMessage.style.display = "block";
        event.preventDefault();
    }
});