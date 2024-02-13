$(document).ready(function () {
  $(".register").hide();
  $(".login_li").addClass("active");

  $(".register_li").on("click", function () {
    $(".register").show();
    $(".login").hide();
    $(".register_li").addClass("active");
    $(".login_li").removeClass("active");
  });

  $(".login_li").on("click", function () {
    $(".register").hide();
    $(".login").show();
    $(".login_li").addClass("active");
    $(".register_li").removeClass("active");
  });

  $(".register .btn a").on("click", function (event) {
    event.preventDefault();
    const username = $('.register input[placeholder="Username"]').val();
    const email = $('.register input[placeholder="E-mail"]').val();
    const password = $('.register input[placeholder="Password"]').val();

    $.ajax({
      url: "http://localhost:5225/api/auth/register",
      method: "POST",
      contentType: "application/json",
      data: JSON.stringify({
        name: username,
        email: email,
        password: password,
      }),
      success: function (response) {
        if (response.message === "User registered successfully") {
          $(".register").hide();
          $(".login").show();
          $(".login_li").addClass("active");
          $(".register_li").removeClass("active");
        }
      },
      error: function (error) {
        console.error(
          "Unable to register, please review registration details",
          error
        );
      },
    });
  });

  $(".login .btn a").on("click", function (event) {
    event.preventDefault();
    const loginEmail = $('.login input[placeholder="E-mail"]').val();
    const loginPassword = $('.login input[placeholder="Password"]').val();

    if (!validateEmail(loginEmail)) {
      $.notify("Invalid email format or email empty", "error");
      return;
    }

    if (!validatePassword(loginPassword)) {
      $.notify("Invalid password format or password empty", "error");
      return;
    }

    $.ajax({
      url: "http://localhost:5225/api/auth/login",
      method: "POST",
      contentType: "application/json",
      data: JSON.stringify({ email: loginEmail, password: loginPassword }),
      success: function (data) {
        if (data.token) {
          localStorage.setItem("token", data.token.tokenString);
          localStorage.setItem("id", data.id);
          window.location.href = "pages/task/task.html";
        } else {
          $.notify("Invalid email or password", "error");
        }
      },
      error: function (error) {
        $.notify(`An error has occurred: ${error.responseJSON.message}`, "error");
      },
    });
  });

  function validateEmail(email) {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailRegex.test(email);
  }

  function validatePassword(password) {
    return password.length >= 6;
  }
});
