import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { CrearProyectoRequest, CrearProyectoResponse, DevuelveProyectosResponse } from '../models/proyectos.model';
import { Observable, tap } from 'rxjs';
import { environment } from '../environments/environments';

@Injectable({
  providedIn: 'root',
})
export class ProyectoService {
  private readonly http = inject(HttpClient);

  crearProyecto(request: CrearProyectoRequest): Observable<CrearProyectoResponse>{
    return this.http.post<CrearProyectoResponse>(`${environment.apiUrl}/Proyecto/crear`, request);
  }  

  devuelveProyectos(secuencialUsuario: number): Observable<DevuelveProyectosResponse>{
    return this.http.get<DevuelveProyectosResponse>(`${environment.apiUrl}/Proyecto/usuario/${secuencialUsuario}`);
  }
}
