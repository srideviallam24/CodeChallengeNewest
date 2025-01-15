
import { Component, OnInit } from '@angular/core';
import { NewStoriesServiceService, Story } from '../../services/new-stories-service.service';
import { EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-new-stories-list',
  standalone: false,
  templateUrl: './new-stories-list.component.html',
  styleUrl: './new-stories-list.component.css'
})
export class NewStoriesListComponent implements OnInit {
  stories: Story[] = [];
  searchText: string = '';
  filteredStories: Story[] = [];
  page: number = 1;  // Current page number
  pageSize: number = 10;  // Number of stories per page
  totalNoOfRecs: number = 1;
  baseStories: number[] = [];
  @Output() filteredStoriesEmitted = new EventEmitter<Story[]>();
  @Output() totalNoOfRecsEmitted = new EventEmitter<number>();
  isLoading = true;
  constructor(private hackerNewsService: NewStoriesServiceService) { }

  ngOnInit(): void {
    this.loadStories();
    setTimeout(() => {
      this.isLoading = false;  // Hide loader after 3 seconds
    }, 3000);
  }

  onSearch(): void {
    this.filteredStories = this.stories.filter(story =>
      story.title.toLowerCase().includes(this.searchText.toLowerCase())
    );
    this.totalNoOfRecs = this.filteredStories.length;
    this.page = 1;
    this.totalNoOfRecsEmitted.emit(this.totalNoOfRecs);
    this.filteredStoriesEmitted.emit(this.filteredStories);
    console.log(this.filteredStories.length);
  }
  loadStories() {
    this.hackerNewsService.getNewestStories(this.page, this.pageSize).subscribe((data) => {
      this.stories = data;
      this.filteredStories = data;
      console.log(this.filteredStories.length);
    });

    this.hackerNewsService.getNewestStoriesCount().subscribe((data) => {
      this.baseStories = data;
      this.totalNoOfRecs = this.baseStories.length;
      console.log(this.baseStories.length);
    });
}
}

