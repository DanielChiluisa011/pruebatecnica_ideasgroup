import { CrearProyectoRequest, Proyecto } from '@/app/models/proyectos.model';
import { ExportColumn, Column } from '@/app/models/table.model';
import { Product, ProductService } from '@/app/pages/service/product.service';
import { AuthService } from '@/app/services/authService';
import { ProyectoService } from '@/app/services/proyectoService';
import { Dialog } from '@/app/shared/components/dialog/dialog';
import { CommonModule } from '@angular/common';
import { Component, inject, OnInit, signal, ViewChild } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { MessageService, ConfirmationService } from 'primeng/api';
import { ButtonModule } from 'primeng/button';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { DatePickerModule } from 'primeng/datepicker';
import { DialogModule } from 'primeng/dialog';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { IconFieldModule } from 'primeng/iconfield';
import { InputIconModule } from 'primeng/inputicon';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextModule } from 'primeng/inputtext';
import { RadioButtonModule } from 'primeng/radiobutton';
import { RatingModule } from 'primeng/rating';
import { RippleModule } from 'primeng/ripple';
import { SelectModule } from 'primeng/select';
import { Table, TableModule } from 'primeng/table';
import { TagModule } from 'primeng/tag';
import { TextareaModule } from 'primeng/textarea';
import { ToastModule } from 'primeng/toast';
import { ToolbarModule } from 'primeng/toolbar';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    CommonModule,
    TableModule,
    FormsModule,
    ButtonModule,
    RippleModule,
    ToastModule,
    ToolbarModule,
    RatingModule,
    InputTextModule,
    TextareaModule,
    SelectModule,
    RadioButtonModule,
    InputNumberModule,
    DialogModule,
    TagModule,
    InputIconModule,
    IconFieldModule,
    ConfirmDialogModule,
    DatePickerModule
  ],
  providers: [MessageService, ProductService, ConfirmationService, DialogService],
  templateUrl: './home.html',
  styleUrl: './home.scss'
})
export class Home implements OnInit {
  private readonly authService = inject(AuthService);
  private readonly router = inject(Router);
  private readonly dialogService = inject(DialogService);
  ref: DynamicDialogRef | undefined;
  productDialog: boolean = false;

  proyectos = signal<Proyecto[]>([]);

  proyecto!: Proyecto;

  selectedProducts!: Proyecto[] | null;

  submitted: boolean = false;

  statuses!: any[];

  @ViewChild('dt') dt!: Table;

  exportColumns!: ExportColumn[];

  cols!: Column[];

  constructor(
    private proyectoService: ProyectoService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService
  ) { }

  exportCSV() {
    this.dt.exportCSV();
  }

  ngOnInit() {
    this.loadDemoData();
  }

  loadDemoData() {
    var usuarioLogueado = this.authService.obtenerUsuarioLogueado()
    if (usuarioLogueado == null) {
      this.router.navigate(["login"]);
      return;
    }

    this.proyectoService.devuelveProyectos(usuarioLogueado.secuencial).subscribe({
      next: (response) => {
        this.proyectos.set(response.proyectos);
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

    this.statuses = [
      { label: 'INSTOCK', value: 'instock' },
      { label: 'LOWSTOCK', value: 'lowstock' },
      { label: 'OUTOFSTOCK', value: 'outofstock' }
    ];

    this.cols = [
      { field: 'code', header: 'Code', customExportHeader: 'Product Code' },
      { field: 'name', header: 'Name' },
      { field: 'image', header: 'Image' },
      { field: 'price', header: 'Price' },
      { field: 'category', header: 'Category' }
    ];

    this.exportColumns = this.cols.map((col) => ({ title: col.header, dataKey: col.field }));
  }

  onGlobalFilter(table: Table, event: Event) {
    table.filterGlobal((event.target as HTMLInputElement).value, 'contains');
  }

  openNew() {
    this.proyecto = {secuencial: 0, nombre: '', descripcion: '', fechaCreacion: undefined, fechaFin: undefined, codigoEstadoProyecto: ''};
    this.submitted = false;
    this.productDialog = true;
  }

  editProduct(proyecto: Product) {
    //this.proyecto = { ...proyecto };
    this.productDialog = true;
  }

  deleteSelectedProducts() {
    this.confirmationService.confirm({
      message: 'Are you sure you want to delete the selected proyectos?',
      header: 'Confirm',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.proyectos.set(this.proyectos().filter((val) => !this.selectedProducts?.includes(val)));
        this.selectedProducts = null;
        this.messageService.add({
          severity: 'success',
          summary: 'Successful',
          detail: 'Products Deleted',
          life: 3000
        });
      }
    });
  }

  hideDialog() {
    this.productDialog = false;
    this.submitted = false;
  }

  deleteProduct(proyecto: Proyecto) {
    this.confirmationService.confirm({
      message: 'Are you sure you want to delete ' + proyecto.nombre + '?',
      header: 'Confirm',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.proyectos.set(this.proyectos().filter((val) => val.secuencial !== proyecto.secuencial));
        this.proyecto = {secuencial: 0, nombre: '', descripcion: '', fechaCreacion: undefined, fechaFin: undefined, codigoEstadoProyecto: ''};
        this.messageService.add({
          severity: 'success',
          summary: 'Successful',
          detail: 'Product Deleted',
          life: 3000
        });
      }
    });
  }

  findIndexById(id: number): number {
    let index = -1;
    for (let i = 0; i < this.proyectos().length; i++) {
      if (this.proyectos()[i].secuencial === id) {
        index = i;
        break;
      }
    }

    return index;
  }

  createId(): string {
    let id = '';
    var chars = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
    for (var i = 0; i < 5; i++) {
      id += chars.charAt(Math.floor(Math.random() * chars.length));
    }
    return id;
  }

  getSeverity(status: string) {
    switch (status) {
      case 'INSTOCK':
        return 'success';
      case 'LOWSTOCK':
        return 'warn';
      case 'OUTOFSTOCK':
        return 'danger';
      default:
        return 'info';
    }
  }

  saveProduct() {
    this.submitted = true;
    const request: CrearProyectoRequest = {
      nombre: this.proyecto.nombre,
      descripcion: this.proyecto.descripcion,
      fechaInicio: this.proyecto.fechaCreacion!,
      fechaFin: this.proyecto.fechaFin!
    }

    this.proyectoService.crearProyecto(request).subscribe({
      next: (response) =>{
        this.ref = this.dialogService.open(Dialog, {
          header: '¡Atención!',
          data: {
            message: "Proyecto creado correctamente."
          },
          width: '400px',
          contentStyle: { overflow: 'auto' },
          baseZIndex: 10000,
          dismissableMask: true // Cierra el diálogo al hacer clic fuera (opcional)
        })!;
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
    })
    
  }
}

