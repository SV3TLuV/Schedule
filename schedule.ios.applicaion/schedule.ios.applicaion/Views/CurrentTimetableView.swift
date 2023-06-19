//
//  CurrentTimetableView.swift
//  schedule.ios.applicaion
//
//  Created by Иван Светлов on 18.06.2023.
//

import SwiftUI

struct CurrentTimetableView: View {
    @State private var currentTimetableData: PagedList<CurrentTimetable>? = nil
    private let api = ScheduleApi()
    var group: Group
    
    var timetables: [Timetable] {
        guard let data = currentTimetableData else {
            return []
        }
        
        return data.items.first!.dates.map { date in
            return date.items.first!
        }
    }
    
    var body: some View {
        ScrollView {
            VStack(spacing: 32) {
                ForEach(timetables) { timetable in
                    TimetableView(timetable: timetable)
                }
            }
            .navigationTitle(timetables.first?.groupNames ?? group.name)
            .navigationBarTitleDisplayMode(.inline)
            .frame(maxWidth: /*@START_MENU_TOKEN@*/.infinity/*@END_MENU_TOKEN@*/)
        }
        .task {
            do {
                currentTimetableData = try await api.fetchCurrentTimetables(groupId: group.id, dateCount: 6)
            } catch {
                
            }
        }
    }
}

//#Preview {
//    CurrentTimetableView()
//}
