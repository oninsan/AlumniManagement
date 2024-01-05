$(()=>{

    $(".submit").click(e=>{
        e.preventDefault()
        const user = $(".username").val();
        const pass = $(".password").val();
    
        $.ajax({
            method:"POST",
            url:"/api/client/userlogin",
            data: {
                "username": user,
                "password": pass
            },
            success:res=>{
                if(res){
                    sessionStorage.setItem("ID",res.ID)
                    sessionStorage.setItem("logged", res.logged)
                    sessionStorage.setItem("role", res.role)
                    sessionStorage.setItem("firstName", res.firstName)
                    sessionStorage.setItem("lastName", res.lastName)
                    sessionStorage.setItem("userName", res.userName)
                    sessionStorage.setItem("password", res.password)
                    sessionStorage.setItem("courseGraduated", res.courseGraduated)
                    sessionStorage.setItem("yearGraduated", res.yearGraduated)
                    sessionStorage.setItem("currentWork", res.currentWork)
                    sessionStorage.setItem("workingStatus", res.workingStatus)
                    console.log(res)
                    if(res.role==="admin"){
                        location.href = "/home" 
                    }else if(res.role == "user"){
                        location.href = "/user"
                    }
                }
            },
            error:err=>{
                console.log(err)
            }
        })
    })     
})
