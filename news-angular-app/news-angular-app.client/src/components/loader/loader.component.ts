import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-loader',
  templateUrl: './loader.component.html',
  styleUrls: ['./loader.component.css'],
  imports:[CommonModule]
})
export class LoaderComponent {
  @Input() isLoading: boolean = false;  // Control loader visibility
}
