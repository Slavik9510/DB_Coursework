import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';

@Component({
  selector: 'app-star-rating',
  templateUrl: './star-rating.component.html',
  styleUrls: ['./star-rating.component.css']
})
export class StarRatingComponent implements OnChanges {
  @Input() rating: number = 0;
  stars: number[] = [];

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['rating']) {
      this.updateStars();
    }
  }

  updateStars(): void {
    this.stars = [];
    let fullStars = Math.floor(this.rating);
    let halfStar = (this.rating % 1) >= 0.5 ? 1 : 0;
    let emptyStars = 5 - fullStars - halfStar;

    for (let i = 0; i < fullStars; i++) {
      this.stars.push(1); // full star
    }
    if (halfStar) {
      this.stars.push(0.5); // half star
    }
    for (let i = 0; i < emptyStars; i++) {
      this.stars.push(0); // empty star
    }
  }

  getStarClass(star: number): string {
    if (star === 1) {
      return 'fas fa-star'; // full star
    } else if (star === 0.5) {
      return 'fas fa-star-half-alt'; // half star
    } else {
      return 'far fa-star'; // empty star
    }
  }
}