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
                console.log(res)
            },
            error:err=>{
                console.log(err)
            }
        })
    })     

})