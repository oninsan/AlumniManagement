$(() => {
    $(".submit").click(e => {
        e.preventDefault();
        const user = $(".username").val();
        const pass = $(".password").val();

        $.ajax({
            method: "POST",
            url: "/api/client/userlogin",
            data: {
                "username": user,
                "password": pass
            },
            success: res => {
                if (res) {
                    // Check if the response contains a message
                    if (res.message) {
                        displayMessage(res.message, "warning");
                    } else if (res.confirmationStatus === 2) {  // Confirmed
                        sessionStorage.setItem("ID", res.ID);
                        sessionStorage.setItem("logged", res.logged);
                        sessionStorage.setItem("role", res.role);
                        sessionStorage.setItem("firstName", res.firstName);
                        sessionStorage.setItem("lastName", res.lastName);
                        sessionStorage.setItem("userName", res.userName);
                        sessionStorage.setItem("password", res.password);
                        sessionStorage.setItem("courseGraduated", res.courseGraduated);
                        sessionStorage.setItem("department", res.department);
                        sessionStorage.setItem("yearGraduated", res.yearGraduated);
                        sessionStorage.setItem("currentWork", res.currentWork);
                        sessionStorage.setItem("workingStatus", res.workingStatus);
                        sessionStorage.setItem("mobileNumber", res.mobileNumber);
                        sessionStorage.setItem("email", res.email);
                        sessionStorage.setItem("picture", res.picture);
                        console.log("result", res);

                        // Redirect based on role
                        if (res.role === "admin") {
                            location.href = "/home";
                        } else if (res.role === "user") {
                            location.href = "/user";
                        }
                    }
                }
            },
            error: err => {
                console.log(err);
            }
        });
    });

    // Function to display message
    function displayMessage(message, type) {
        var messageElement = $('.message');

        // If message element doesn't exist, create it
        if (messageElement.length === 0) {
            messageElement = $('<div class="message"></div>');
            $('.login-form').prepend(messageElement); // Add message before form
        }

        // Add message content and style based on type
        messageElement.text(message);
        messageElement.removeClass("alert-success alert-danger alert-warning");  // Remove previous alert types
        messageElement.addClass("alert alert-" + type);  // Add new alert type class

        // Show the message element
        messageElement.show();
    }
});
