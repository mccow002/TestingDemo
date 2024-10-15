import {Component, Input} from '@angular/core';
import {BookViewModel} from "../models";
import { CommonModule, NgOptimizedImage } from "@angular/common";

@Component({
  selector: 'app-book-details',
  templateUrl: './book-details.component.html',
  styleUrls: ['./book-details.component.scss'],
  imports: [
    CommonModule,
    NgOptimizedImage
  ],
  standalone: true
})
export class BookDetailsComponent {
  @Input() book!: BookViewModel;
}
