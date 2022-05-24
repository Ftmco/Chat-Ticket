﻿using ChTi.Service.Tools.Date;
using Identity.Service.Tools.Code;

namespace ChTi.Service.Implemention;

public class TicketViewModelService : ITicketViewModel
{
    readonly IUserGet _userGet;

    readonly IBaseQuery<Chat> _chatQuery;

    readonly IBaseCud<Chat> _chatCud;

    readonly IChatUserAction _chatUsers;

    public TicketViewModelService(IUserGet userGet, IBaseQuery<Chat> chatQuery, IBaseCud<Chat> chatCud, IChatUserAction chatUsers)
    {
        _userGet = userGet;
        _chatQuery = chatQuery;
        _chatCud = chatCud;
        _chatUsers = chatUsers;
    }

    public async Task<Chat?> CreateChatAsync(UpsertChatViewModel create, IEnumerable<AddUserToChatViewModel> addUserToChat)
    {
        Chat chat = new()
        {
            CreateDate = DateTime.Now,
            Description = create.Description,
            Name = create.Name,
            Status = (short)ChatStatus.Active,
            Token = 50.CreateToken(),
            Type = (short)create.Type,
            UpdateDate = DateTime.Now,
        };
        if (await _chatCud.InsertAsync(chat))
        {
            foreach (var user in addUserToChat)
                if (user.ChatId == null)
                    await _chatUsers.AddUserToChatAsync(user with { ChatId = chat.Id });
                else
                    await _chatUsers.AddUserToChatAsync(user);

            return chat;
        }
        return null;
    }

    public async Task<TicketDetailViewModel> CreateTicketDetailViewModelAsync(Ticket ticket)
    {
        User? fromUser = await _userGet.GetUserByIdAsync(ticket.FromUserId);
        User? toUser = await _userGet.GetUserByIdAsync(ticket.ToUserId);
        TicketDetailViewModel ticketDetail = new(
            Ticket: await CreateTicketViewModelAsync(ticket),
            FromUser: await CreateUserViewModelAsync(fromUser),
            ToUser: await CreateUserViewModelAsync(toUser));
        return ticketDetail;
    }

    public async Task<TicketViewModel> CreateTicketViewModelAsync(Ticket ticket)
    {
        Chat? chat = await _chatQuery.GetAsync(ticket.ChatId);
        if (chat == null)
        {
            chat = await CreateChatAsync(new(Id: null, Name: ticket.Subject,
                Description: ticket.Description, Type: ChatType.Pv), new List<AddUserToChatViewModel>
                {
                    new(ChatId:null,ticket.FromUserId,ChatUserType.Owner),
                    new(ChatId:null,ticket.ToUserId,ChatUserType.User),
                });
        }
        TicketViewModel viewModel = new(
            Id: ticket.Id,
            FromId: ticket.FromUserId,
            ToId: ticket.ToUserId, Subject: ticket.Subject,
            ChatToken: chat?.Token ?? "",
            Description: ticket.Description,
            CreateDate: ticket.CreateDate.ToShamsi(),
            Status: new(ticket.Status, (TicketStatus)ticket.Status switch
            {
                TicketStatus.Open => "باز",
                TicketStatus.Close => "بسته شده",
                _ => "باز"
            }));
        return viewModel;
    }

    public async Task<IEnumerable<DataBase.ViewModel.TicketViewModel>> CreateTicketViewModelAsync(IEnumerable<Ticket> tickets)
    {
        List<DataBase.ViewModel.TicketViewModel> result = new();
        foreach (var item in tickets)
            result.Add(await CreateTicketViewModelAsync(item));
        return result;
    }

    public async Task<UserViewModel?> CreateUserViewModelAsync(User? user)
    {
        if (user == null)
            return null;

        IEnumerable<Avatar>? userAvatars = await _userGet.GetUserAvatarsAsync(user.Id, false);
        UserViewModel userViewModel = new(
            Id: user.Id,
            FullName: user.FullName,
            Email: user.Email,
            MobileNo: user.MobileNo,
            Avatar: userAvatars?.Select((avatr) =>
                            new FileViewModel(FileId: avatr.FileId, FileToken: avatr.FileToken)));
        return userViewModel;
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }
}
