export interface Usuario {
    secuencial: number,
    nombre: string,
    correo: string,
    password: string
}

export interface UsuarioLogin {
    correo: string,
    password: string
}

export interface UsuarioRegister{
    nombre: string,
    correo: string,
    password: string
}

export interface UsuarioResponse {
    secuencial: number,
    nombre: string,
    correo: string,
    password: string,
    token: string
} 