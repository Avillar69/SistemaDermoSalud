
let ArrayPermisos = JSON.parse(localStorage.getItem("PermisosModulos"));
let ModuloInicial = JSON.parse(localStorage.getItem("moduloInicial"));
let Menu_Id_Empresa = 0;
let user = "";
let password = "";
let rememberme = "";


$(document).ready(function () {
    Menu_Id_Empresa = localStorage.getItem("IdEmpresa");
    $("#EmpresaSeleccionada").text(localStorage.getItem("Empresa"));
    ModuloInicial = localStorage.getItem("moduloInicial");
    user = localStorage.getItem("user");
    password = localStorage.getItem("password");
    rememberme = localStorage.getItem("rememberme");
    rememberme = localStorage.getItem("rememberme");

    PintarModulos();
    PintarPermisosModulos();
    MultiEmpresa(ModuloInicial);
    $(".modulo").click(function () {        
        ModuloInicial = $(this).attr("data-id");
        $(".menu").empty();
        
        MultiEmpresa(ModuloInicial);
        UpdateModuloPorDefecto(ModuloInicial);
        PintarPermisosByModulo(ModuloInicial);
    });
        
    $(".logout, #BtnLogoutMenu").click(function () {
        Logout();
    });

    $(".cambiarempresa").click(function () {
        ListarEmpresas();
        $("#ModalEmpresa").modal("show");
    });

    $(".cambiarcontra").click(function () {
        $("#ModalCambiarContra").modal("show");
    });

    $(".updatecontra").click(function () {
        UpdatePassword();
    });
})

function PintarModulos() {
    let modulos = ``;
    if (ArrayPermisos != null) {
        //Pintando Niveles
        modulos += `<div class="row g-0">`;
        ArrayPermisos.forEach(function (value, index) {           
            if (value.Nivel == 0) {               
                modulos += `<div class="col-4">
                                <a class="dropdown-icon-item modulo" data-id="${value.Codigo}" data-codigomodulo="${value.CodigoModulo}">
                                    <img src="/assets/images/brands/${value.Icono}">
                                        <span>${value.Descripcion}</span>
                                </a>
                            </div>`;
            }
        });

        modulos += `</div>`;
        $("#modulos").append(modulos);       
        //Fin Pintando Niveles
    }
    //AgregarActiveModulo();
}
function PintarPermisosModulos() {
    if (ArrayPermisos != null) {
        //Pintando Niveles
        ArrayPermisos.forEach(function (value, index) {  

            if (value.Nivel == 1 && value.CodPadre == ModuloInicial) {
                let Template =
                    `<li class="nav-item">
                        <a  ${value.SubOpcion == true ? `class="nav-link menu-link collapsed" data-codigo-padre="${value.CodPadre}" data-codigo="${value.Codigo}" href="#${value.Descripcion}" data-bs-toggle="collapse" role="button" aria-expanded="false" aria-controls="${value.CodigoModulo}"`
                        : `class= "nav-link menu-link" data-codigo-padre="${value.CodPadre}" data-codigo="${value.Codigo}" href="/${value.Url}" data-menu-url="/${value.Url}" 
                        data-menu-link="true" data-menu-id="${value.ModulosID}" data-menu-codigo-modulo="${value.CodigoModulo}"`}><i class="${value.Icono}"></i>${value.Descripcion}
                        <span id="Span_Menu_${value.CodigoModulo}"></span></a>
                        ${value.SubOpcion == true ? `<div class="collapse menu-dropdown" id="${value.CodigoModulo}"><ul class="nav nav-sm flex-column ul-seccion-${value.Codigo}"></ul></div>` : ``}
                    </li>`
                $(".ul-seccion-" + value.CodPadre).append(Template);

            } else if (value.Nivel == 2) {
                let Template =
                    `<li class="nav-item">
                        <a ${value.SubOpcion == true ? `class="nav-link menu-link collapsed" data-codigo-padre="${value.CodPadre}" data-codigo="${value.Codigo} href="#${value.Descripcion}" data-bs-toggle="collapse" role="button" aria-expanded="false" aria-controls="${value.Descripcion}"`
                        : `class="nav-link" data-codigo-padre="${value.CodPadre}" data-codigo="${value.Codigo}" href="/${value.Url}" data-menu-url="/${value.Url}" 
                        data-menu-link="true" data-menu-id="${value.ModulosID}" data-menu-codigo-modulo="${value.CodigoModulo}"`}><i class="${value.Icono}"></i>${value.Descripcion}
                        </a>
                        ${value.SubOpcion == true ? `<div class="collapse menu-dropdown" id="${value.CodigoModulo}"><ul class="nav nav-sm flex-column ul-seccion-${value.Codigo}"></ul></div>` : ``}
                    </li>`

                $(".ul-seccion-" + value.CodPadre).append(Template);
            } else if (value.Nivel == 3) {
                let Template =
                    `<li class="nav-item">
                        <a ${value.SubOpcion == true ? `class="nav-link menu-link collapsed" data-codigo-padre="${value.CodPadre}" data-codigo="${value.Codigo}" href="#${value.Descripcion}" data-bs-toggle="collapse" role="button" aria-expanded="false" aria-controls="${value.Descripcion}"` : `class="nav-link" data-codigo-padre="${value.CodPadre}" data-codigo="${value.Codigo}" href="/${value.Url}" data-menu-url="/${value.Url}" data-menu-link="true" data-menu-id="${value.ModulosID}" data-menu-codigo-modulo="${value.CodigoModulo}"`}><i class="${value.Icono}"></i>${value.Descripcion}</a>
                        ${value.SubOpcion == true ? `<div class="collapse menu-dropdown" id="${value.CodigoModulo}"><ul class="nav nav-sm flex-column ul-seccion-${value.Codigo}"></ul></div>` : ``}
                    </li>`

                $(".ul-seccion-" + value.CodPadre).append(Template);
            } else if (value.Nivel == 4) {
                let Template =
                    `<li class="nav-item">
                        <a ${value.SubOpcion == true ? `class="nav-link menu-link collapsed" data-codigo-padre="${value.CodPadre}" data-codigo="${value.Codigo}" href="#${value.Descripcion}" data-bs-toggle="collapse" role="button" aria-expanded="false" aria-controls="${value.Descripcion}"` : `data-codigo-padre="${value.CodPadre}" data-codigo="${value.Codigo}" href="/${value.Url}" data-menu-url="/${value.Url}" data-menu-link="true" data-menu-id="${value.ModulosID}" `}><i class="${value.Icono}"></i><${value.Descripcion}</a>
                       ${value.SubOpcion == true ? `<div class="collapse menu-dropdown" id="${value.CodigoModulo}"><ul class="nav nav-sm flex-column ul-seccion-${value.Codigo}"></ul></div>` : ``}
                    </li>`

                $(".ul-seccion-" + value.CodPadre).append(Template);
            }      
        });
        //Fin Pintando Niveles
    }
    AgregarActiveModulo();
}
function GetPermisoPorCodigo(CodigoModulo) {

    let Permiso = ArrayPermisos.find(x => x.CodigoModulo == CodigoModulo);

    if (Permiso != null && Permiso != undefined) {
        return Permiso;
    } else {
        return {
            "Descripcion": "",
            "Permisos": {
                "Aniadir": false,
                "Eliminar": false,
                "Modificar": false,
                "Ver": false,
                "Detalle": false,
                "Aprobar": false,
                "Exportar": false
            }
        }
    }

}
function AgregarActiveModulo() {
    
    $(`a[data-menu-url='${window.location.pathname}']`).parent().addClass("active");

    let IdCodigoPadre_1 = $(`a[data-menu-url='${window.location.pathname}']`).attr("data-codigo-padre");
    $(`a[data-codigo='${IdCodigoPadre_1}']`).parent().addClass("active");

    let IdCodigoPadre_2 = $(`a[data-codigo='${IdCodigoPadre_1}']`).attr("data-codigo-padre");
    $(`a[data-codigo='${IdCodigoPadre_2}']`).parent().addClass("active");

    let IdNavLink = $(`a[data-menu-url='${window.location.pathname}']`).parents(".nav-link").attr("id");

    $(`a.nav-link[href='#${IdNavLink}']`).addClass("active");
    $(`a[data-menu-url='${window.location.pathname}']`).parents(".nav-link").addClass("active");

    if ($(`a[data-menu-url='${window.location.pathname}']`).parents("#rightbar").length != 0) {
        $(`#NavSeccion-1`).addClass("active");
        $("#seccion-1").addClass("active");
    }

    if ($(`a[data-menu-url='${window.location.pathname}']`).parents("#left-sidebar").length != 0) {
        $(`#NavSeccion-3`).addClass("active");
        $("#seccion-3").addClass("active");
    }

    if ($(`a[data-menu-url='${window.location.pathname}']`).parents("#rightbar").length == 0 && $(`a[data-menu-url='${window.location.pathname}']`).parents("#left-sidebar").length == 0) {
        $(`#NavSeccion-1`).addClass("active");
        $("#seccion-1").addClass("active");
        $(`#NavSeccion-3`).addClass("active");
        $("#seccion-3").addClass("active");
    }

    if (ArrayPermisos != null) {
        //Pintando Niveles
        ArrayPermisos.forEach(function (value, index) {
            if (value.Nivel == 0 && value.CodigoModulo == "FAQ") {               
                let Size_Contenedor = document.getElementById("main-content");
                _size = Size_Contenedor.clientWidth - 1080;

                let contenedor_faq = "";
               
                if (value.CodigoModulo == "FAQ") {
                    contenedor_faq += `<div style=" position: fixed;bottom: 10px; right: 50px;"><strong>${Version_Sistema}</strong></div>`;
                    /*contenedor_faq += `<div style="position: relative;top: ${_size}px;height:20px;"></div>`;*/
                }
                $("#seccion-" + value.Codigo).append(contenedor_faq);
            }
        })
    }
}
function PintarPermisosByModulo(ModuloID) {
    if (ArrayPermisos != null) {
        //Pintando Niveles
        ArrayPermisos.forEach(function (value, index) {
            if (value.Nivel == 1 && value.CodPadre == ModuloID) {
                let Template =
                    `<li class="nav-item">
                        <a  ${value.SubOpcion == true ? `class="nav-link menu-link collapsed" data-codigo-padre="${value.CodPadre}" data-codigo="${value.Codigo}" href="#${value.Descripcion}" data-bs-toggle="collapse" role="button" aria-expanded="false" aria-controls="${value.CodigoModulo}"`
                        : `class= "nav-link menu-link" data-codigo-padre="${value.CodPadre}" data-codigo="${value.Codigo}" href="/${value.Url}" data-menu-url="/${value.Url}" 
                        data-menu-link="true" data-menu-id="${value.ModulosID}" data-menu-codigo-modulo="${value.CodigoModulo}"`}><i class="${value.Icono}"></i>${value.Descripcion}
                        <span id="Span_Menu_${value.CodigoModulo}"></span></a>
                        ${value.SubOpcion == true ? `<div class="collapse menu-dropdown" id="${value.CodigoModulo}"><ul class="nav nav-sm flex-column ul-seccion-${value.Codigo}"></ul></div>` : ``}
                    </li>`
                $(".ul-seccion-" + value.CodPadre).append(Template);

            } else if (value.Nivel == 2) {
                let Template =
                    `<li class="nav-item">
                        <a ${value.SubOpcion == true ? `class="nav-link menu-link collapsed" data-codigo-padre="${value.CodPadre}" data-codigo="${value.Codigo} href="#${value.Descripcion}" data-bs-toggle="collapse" role="button" aria-expanded="false" aria-controls="${value.Descripcion}"`
                        : `class="nav-link" data-codigo-padre="${value.CodPadre}" data-codigo="${value.Codigo}" href="/${value.Url}" data-menu-url="/${value.Url}" 
                        data-menu-link="true" data-menu-id="${value.ModulosID}" data-menu-codigo-modulo="${value.CodigoModulo}"`}><i class="${value.Icono}"></i>${value.Descripcion}
                        </a>
                        ${value.SubOpcion == true ? `<div class="collapse menu-dropdown" id="${value.CodigoModulo}"><ul class="nav nav-sm flex-column ul-seccion-${value.Codigo}"></ul></div>` : ``}
                    </li>`

                $(".ul-seccion-" + value.CodPadre).append(Template);
            } else if (value.Nivel == 3) {
                let Template =
                    `<li class="nav-item">
                        <a ${value.SubOpcion == true ? `class="nav-link menu-link collapsed" data-codigo-padre="${value.CodPadre}" data-codigo="${value.Codigo}" href="#${value.Descripcion}" data-bs-toggle="collapse" role="button" aria-expanded="false" aria-controls="${value.Descripcion}"` : `class="nav-link" data-codigo-padre="${value.CodPadre}" data-codigo="${value.Codigo}" href="/${value.Url}" data-menu-url="/${value.Url}" data-menu-link="true" data-menu-id="${value.ModulosID}" data-menu-codigo-modulo="${value.CodigoModulo}"`}><i class="${value.Icono}"></i>${value.Descripcion}</a>
                        ${value.SubOpcion == true ? `<div class="collapse menu-dropdown" id="${value.CodigoModulo}"><ul class="nav nav-sm flex-column ul-seccion-${value.Codigo}"></ul></div>` : ``}
                    </li>`

                $(".ul-seccion-" + value.CodPadre).append(Template);
            } else if (value.Nivel == 4) {
                let Template =
                    `<li class="nav-item">
                        <a ${value.SubOpcion == true ? `class="nav-link menu-link collapsed" data-codigo-padre="${value.CodPadre}" data-codigo="${value.Codigo}" href="#${value.Descripcion}" data-bs-toggle="collapse" role="button" aria-expanded="false" aria-controls="${value.Descripcion}"` : `data-codigo-padre="${value.CodPadre}" data-codigo="${value.Codigo}" href="/${value.Url}" data-menu-url="/${value.Url}" data-menu-link="true" data-menu-id="${value.ModulosID}" `}><i class="${value.Icono}"></i><${value.Descripcion}</a>
                       ${value.SubOpcion == true ? `<div class="collapse menu-dropdown" id="${value.CodigoModulo}"><ul class="nav nav-sm flex-column ul-seccion-${value.Codigo}"></ul></div>` : ``}
                    </li>`

                $(".ul-seccion-" + value.CodPadre).append(Template);
            }
        });
        //Fin Pintando Niveles
    }
    AgregarActiveModulo();
}
function MultiEmpresa(ModuloID) {
    let modulo = ArrayPermisos.filter(x => x.ModulosID == ModuloID);
    if (modulo[0].MultiEmpresa && Menu_Id_Empresa == 0) {
        ListarEmpresas();
        $("#ModalEmpresa").modal("show");
    }
    else {
        $("#ModalEmpresa").modal("hide");
    }
}
function ListarEmpresas() {
    $.ajax({
        type: 'GET',
        url: `../Empresa/GetAll`,
        contentType: "application/json; charset=utf-8",
        async: true,
        cache: false,
        beforeSend: function () {
            $(`#candidate-list`).empty();
        },
        success: function (response) {
            if (response.resultado) {
                PintaEmpresa(response.data);
            } else {
                console.log(response);
            }
        },
        error: function (xhr, status, error) {
        },
        complete: function () {
        }
    });
}
function PintaEmpresa(data) {
    if (data != null && data != undefined) {
        let empresa = ``;
        data.forEach(function (value, index) {
            if (value.tipoSituacionID == 'ACT') {
                empresa += `<li>
							    <div class="d-flex align-items-center">
                                    <div class="flex-grow-1">
								        <h5 class="fs-11 mb-1 text-truncate"><span class="candidate-name">${value.razonSocial}</span></h5>
                                    </div>
                                    <div class="flex-shrink-0 ms-2">
								        <button type="button" class="btn btn-primary waves-effect waves-light fs-11 m-1 empresa" data-idempresa="${value.empresaID}" data-empresa="${value.razonSocial}" onclick="CambiarEmpresa(${value.empresaID}, '${value.razonSocialNombreCorto}')">Seleccionar</button>
                                    </div>
                                </div>
                            </li>`;
            }            
        });

        $("#candidate-list").append(empresa);
    }
}
function CambiarEmpresa(IdEmpresa, Empresa) {
    Menu_Id_Empresa = IdEmpresa;
    localStorage.setItem("IdEmpresa", IdEmpresa);
    localStorage.setItem("Empresa", Empresa);
    $("#EmpresaSeleccionada").text(localStorage.getItem("Empresa"));
    $("#ModalEmpresa").modal("hide");
}
function Logout() {
    debugger
    localStorage.setItem("IdEmpresa", 0);
    localStorage.setItem("Empresa", "");

    $.ajax({
        type: 'GET',
        url: `../Account/Logout`,
        contentType: "application/json; charset=utf-8",
        async: true,
        cache: false,
        beforeSend: function () {
        },
        success: function (response) {
        },
        error: function (xhr, status, error) {
        },
        complete: function () {
        }
    });
}
function UpdatePassword() {
    let validacion = true;

    let model = {
        UsuarioID: G_Menu_Id_Usuario_Logeado,
        ColaboradorID: G_IdUsuario_Logeado,
        Password: $("#Nuevo_Password_Change").val(),
        ConfirmPassword: $("#Nuevo_ConfirmarPassword_Change").val(),
        Updated_by: G_IdUsuario_Logeado
    };

    if (model.Password.trim().length != 0) {
        if (model.Password.trim() != model.ConfirmPassword.trim()) {
            validacion = false;
            Toastr_NotificacionError("¡Aviso del Sistema!", `Las contraseñas no coinciden`, false);
        }
    }

    if (validacion) {
        Swal.fire({
            title: "¿Estás seguro?",
            text: "No podrás revertir este cambio",
            icon: "warning",
            showCancelButton: true,
            cancelButtonText: "NO",
            confirmButtonText: "SI",
            showLoaderOnConfirm: true,
        }).then((result) => {
            if (result.isConfirmed) {
                Swal.fire({
                    html: 'Espere por favor...',
                    allowOutsideClick: false,
                    allowEscapeKey: false,
                    allowEnterKey: false,
                    showConfirmButton: false,
                    onBeforeOpen: () => {
                        Swal.showLoading()
                    }
                });

                $.ajax({
                    type: 'POST',
                    url: `../Usuario/UpdatePassword`,
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify(model),
                    async: true,
                    cache: false,
                    beforeSend: function () {
                    },
                    success: function (response) {
                        if (response.resultado) {
                            $("#ModalCambiarContra").modal("hide");
                            Swal.fire("¡Aviso del Sistema!", "Se actualizó la contraseña correctamente.", "success"); 
                            document.getElementById("BtnLogoutMenu").click();
                        }
                        else {
                            if (response.errorUsuarioExiste) {
                                Swal.fire("¡Aviso del Sistema!", response.data, "warning");
                            } else if (response.errorContrasenia) {
                                Swal.fire("¡Aviso del Sistema!", response.data, "warning");
                            }
                            else {
                                Swal.fire('¡Aviso del Sistema!', 'Ocurrió un error', 'error')
                            }
                        }
                    },
                    error: function (xhr, status, error) {
                        Swal.fire('¡Aviso del Sistema!', 'Ocurrió un error', 'error')
                    },
                    complete: function () {
                    }
                });
            }
        });
    }
}
function UpdateModuloPorDefecto(ModuloID) {
    let model = {
        PerfilID: G_IdPerfil_Logeado,
        ModuloInicial: ModuloID
    };

    $.ajax({
        type: 'POST',
        url: `../Perfil/UpdateModuloPorDefecto`,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(model),
        async: true,
        cache: false,
        beforeSend: function () {
        },
        success: function (response) {
            if (response.resultado) {
                ReSesion();
            }
        },
        error: function (xhr, status, error) {
            Swal.fire('¡Aviso del Sistema!', 'Ocurrió un error', 'error')
        },
        complete: function () {
        }
    });
}
function ReSesion() {
    let model = {
        "UserName": user,
        "Password": password,
        "RememberMe": rememberme
    };

    $.ajax({
        type: 'POST',
        url: `../Account/ReSesion?UserName=${model.UserName}&Password=${model.Password}&RememberMe=${model.RememberMe}`,
        contentType: "application/json; charset=utf-8",
        //data: JSON.stringify(model),
        async: true,
        cache: false,
        beforeSend: function () {
            localStorage.removeItem("PermisosModulos");
            localStorage.removeItem("paginaDefecto");
            localStorage.removeItem("moduloInicial");
        },
        success: function (response) {
            if (response.resultado) {
                localStorage.setItem("PermisosModulos", response.permisosModulos);
                localStorage.setItem("paginaDefecto", response.paginaDefecto);
                localStorage.setItem("moduloInicial", response.moduloInicial);                

                ArrayPermisos = JSON.parse(localStorage.getItem("PermisosModulos"));                
                window.location.href = `../${response.paginaDefecto}`;
            }
        },
        error: function (xhr, status, error) {
        },
        complete: function () {
        }
    });
}