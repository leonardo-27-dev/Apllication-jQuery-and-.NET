$(document).ready(function () {
  if (
    window.location.pathname === "/pages/task/task.html" &&
    !localStorage.getItem("token")
  ) {
    $.notify(
      "Você não tem permissão para acessar esta página. Por favor, efetue o login ou realize uma nova autenticação",
      "error"
    );
    setTimeout(function () {
      window.location.href = "/index.html";
    }, 4000);
  }
});
