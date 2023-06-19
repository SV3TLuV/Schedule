//
//  GroupViewModel.swift
//  schedule.ios.applicaion
//
//  Created by Иван Светлов on 18.06.2023.
//

import Foundation

@MainActor class GroupViewModel: ObservableObject {
    @Published var groups: [Group] = []
    @Published var isRequestFailed = false
    @Published var isHasMore = true
    
    private var groupData: PagedList<Group>? = nil
    private let api = ScheduleApi()
    
    func fetchGroups(search: String = "") {
        Task {
            do {
                groupData = try await api.fetchGroups(search: search)
                groups = groupData!.items
                isRequestFailed = false
            } catch {
                isRequestFailed = true
            }
            isHasMore = hasMore()
        }
    }
    
    func loadMore(search: String = "") {
        Task {
            guard let data = groupData else {
                return
            }
            
            if !isHasMore {
                return
            }
            
            let nextPage = data.pageNumber + 1
            
            do {
                groupData = try await api.fetchGroups(page: nextPage, search: search)
                groups.append(contentsOf: groupData!.items)
                isRequestFailed = false
            } catch {
                isRequestFailed = true
            }
            isHasMore = hasMore()
        }
    }
    
    private func hasMore() -> Bool {
        guard let data = groupData else {
            return true
        }
        return data.pageNumber < data.totalPages
    }
}
