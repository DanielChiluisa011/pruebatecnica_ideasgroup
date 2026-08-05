import { Component, inject, Input, OnInit } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';

@Component({
  selector: 'app-dialog',
  imports: [DialogModule,ButtonModule],
  templateUrl: './dialog.html',
  styleUrl: './dialog.scss',
})
export class Dialog implements OnInit {
  @Input() display: boolean = false;
  message: string = '';
  constructor(
    private ref: DynamicDialogRef,
    private config: DynamicDialogConfig 
  ) {}
  ngOnInit(): void {
    this.message = this.config.data.message;
  }

  close() {
    this.ref.close();
  }
  open() {
    this.display = true;
  }
}
