import { AppFloatingConfigurator } from '@/app/layout/component/app.floatingconfigurator';
import { UsuarioLogin } from '@/app/models/usuario.model';
import { AuthService } from '@/app/services/authService';
import { Dialog } from '@/app/shared/components/dialog/dialog';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { CheckboxModule } from 'primeng/checkbox';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { DialogService, DynamicDialogModule, DynamicDialogRef } from 'primeng/dynamicdialog'
import { RippleModule } from 'primeng/ripple';

@Component({
  selector: 'app-login',
  imports: [ButtonModule, CheckboxModule, InputTextModule, PasswordModule, FormsModule, RouterModule, RippleModule, AppFloatingConfigurator],
  templateUrl: './login.html',
  styleUrl: './login.scss',
  providers: [DialogService]
})
export class Login {
  private readonly authService = inject(AuthService);
  private readonly router = inject(Router);
  private readonly dialogService = inject(DialogService);
  ref: DynamicDialogRef | undefined; 
  correo: string = '';
  password: string = '';
  display: boolean = false;
  message: string = '';
  login() {
    const request: UsuarioLogin = {
      correo: this.correo,
      password: this.password
    };
    this.authService.login(request).subscribe({
      next: (response) => {
        console.log('Login successful:', response);
        this.router.navigate(['/projects']);
      },
      error: (error) => {
        console.error('Login failed:', error);
        this.ref = this.dialogService.open(Dialog, {
        header: '¡Atención!',
        data: {
          message: error.error.message
        },
        width: '400px',
        contentStyle: { overflow: 'auto' },
        baseZIndex: 10000,
        dismissableMask: true // Cierra el diálogo al hacer clic fuera (opcional)
      })!;
      }
    });
  }
}
