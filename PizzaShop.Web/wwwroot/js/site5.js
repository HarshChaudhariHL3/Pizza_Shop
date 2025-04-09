const menuBtn = document.getElementById('menu-btn');
const aside = document.getElementById('nav-bar');
const mainPart = document.getElementById('main-part');
const small_container = document.getElementById('small-container'); 
const logoSection = document.getElementById("logo-section");
let show = true;


menuBtn.addEventListener('click',()=>{
    if(show){
        // console.log(aside);
        
        mainPart.classList.remove("col-lg-9","col-xxl-10");
        mainPart.classList.add("col-12");
        aside.classList.remove("col-lg-3", "col-xxl-2");
        aside.classList.add("d-none");
        // small_container.classList.remove("d-block","d-lg-none");
        // small_container.classList.add("d-block");
        logoSection.classList.remove("d-none");
        logoSection.classList.add("d-flex");

        show = !show ;  


    }
    else{
        aside.classList.remove("d-none")
        aside.classList.add("col-lg-3","col-xxl-2");
        mainPart.classList.remove("col-12");
        mainPart.classList.add("col-lg-9", "col-xxl-10");
        logoSection.classList.add("d-none");
        logoSection.classList.remove("d-flex");
        // small_container.classList.remove("d-block");
        // small_container.classList.add("d-block","d-lg-none");
        show = !show;
    }
})


const inp = document.getElementById("actual-btn");
const tag = document.getElementById("changed-tag");

// inp.addEventListener('change',()=>
//     {
//         let fileName = inp.files[0].name;
//         tag.innerHTML = fileName;
//     });



    $(document).ready(function () {
        $(".categorylistitem a").each(function () {
          if (this.href === window.location.href) {
            $(this).addClass("active");
            $(this).parent().addClass("active");
          }
        });
    });



$(".showEye").click(function () {
    console.log("button click");
  
    $(this).toggleClass("bi bi-eye-slash-fill");
    if ($(".pass").attr("type") == "password") {
      $(".pass").attr("type", "text");
    } else {
      $(".pass").attr("type", "password");
    }
  });


