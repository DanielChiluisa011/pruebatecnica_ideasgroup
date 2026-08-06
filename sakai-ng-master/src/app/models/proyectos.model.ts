export interface Proyecto {
    secuencial: number,
    nombre: string,
    descripcion: string,
    fechaCreacion?: Date,
    fechaFin?: Date,
    codigoEstadoProyecto: string
}

export interface CrearProyectoRequest {
    nombre: string,
    descripcion: string,
    fechaInicio: Date,
    fechaFin: Date,
}

export interface ActualizarProyectoRequest {
    proyecto: Proyecto
}

export interface CrearProyectoResponse{
    message: string,
}


export interface DevuelveProyectosResponse{
    proyectos: Proyecto[]
}