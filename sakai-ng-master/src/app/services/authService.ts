import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { UsuarioLogin, UsuarioResponse } from '../models/usuario.model';
import { Observable, tap } from 'rxjs';
import { environment } from '../environments/environments';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly http = inject(HttpClient);
  private readonly TOKEN_KEY = '';

  login(request: UsuarioLogin): Observable<UsuarioResponse> {
    return this.http.post<UsuarioResponse>(`${environment.apiUrl}/Usuarios/login`, request)
    .pipe(
      tap(response => {
        localStorage.setItem("usuario", JSON.stringify(response));
      })
    )
  }

  logout(): void {
    localStorage.removeItem(this.TOKEN_KEY);
  }

  getToken(): string | null {
    return localStorage.getItem(this.TOKEN_KEY);
  }

  isAutenticated(): boolean {
    const token = this.getToken();
    return !!token;
  }
  obtenerUsuarioLogueado(): UsuarioResponse | null {
    const data = localStorage.getItem("usuario");
    return data ? JSON.parse(data) as UsuarioResponse : null;
  }

}
